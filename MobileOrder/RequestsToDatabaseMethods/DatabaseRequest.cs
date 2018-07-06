using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using MobileOrder.Globals;
using MobileOrder.ViewModels;
using MobileOrder.Model;
using MobileOrder.CustomExceptions;

namespace MobileOrder.RequestsToDatabaseMethods
{
	public class DatabaseRequest
	{
		public static List<T> GetAllFromTable<T>() where T: new()
		{
			using (var connection = new SQLiteConnection(System.IO.Path.Combine(GlobalVariables.databasePath, "MobileSell.db")))
			{
				try
				{
					var all = connection.Table<T>().ToList();
					return all;
				}
				catch (Exception ex)
				{
					return null;

				}
			}
		}

		/// <summary>
		/// Decrease quantitiy from table articles and sertifs for passed articles
		/// </summary>
		/// <param name="orderArticles">List of articles in the order</param>
		public static void UpdateQuantities(List<OrderArticleViewModel> orderArticles)
		{
			using (var connection = new SQLiteConnection(System.IO.Path.Combine(GlobalVariables.databasePath, "MobileSell.db")))
			{
				////try
				////{
				////	string query = "UPDATE " + tableName + " SET " + columnName + "=" + id + "WHERE Id = ?", id, value
				//connection.Execute("UPDATE" + tableName + "SET columnName = ? Where Id = ?", id, value);
				////}
				////catch (Exception ex)
				////{
				////	return null;

				////}

				//connection.BeginTransaction();

				//try
				//{
				//	//string query = "UPDATE " + tableName + " SET " + columnName + "=" + id + "WHERE Id = ?", id, value
				//	//connection.Execute("UPDATE" + tableName + "SET columnName = ? Where Id = ?", id, value);
				//	Article article = new Article();

				//	connection.Update(article);
				//}
				//catch (Exception ex)
				//{
				//	//return null;

				//}

				connection.BeginTransaction();
				try
				{
					foreach (OrderArticleViewModel orderArticle in orderArticles)
					{
						if (orderArticle.LotId != null)
						{
							//if orderArticle has lot, decreese its quantity from tables articles and sertif
							Sertif sertif = connection.Table<Sertif>().Where(x => x.Lotid == orderArticle.LotId).FirstOrDefault();
							decimal lotQuantityAvailable = sertif.Quantity;
							sertif.Quantity -= Convert.ToDecimal(orderArticle.ArticleQuantity);
							if (sertif.Quantity >= 0)
							{
								connection.Update(sertif);
							}
							else
							{
								connection.Rollback();
								throw new InsufficientQuantityException(orderArticle.ArticleQuantity, orderArticle.ArticleName, lotQuantityAvailable);
							}

							Article article = connection.Table<Article>().Where(x => x.Id == orderArticle.ArticleId).FirstOrDefault();
							decimal articleQuantityAvailable = article.Quantity;
							article.Quantity -= Convert.ToDecimal(orderArticle.ArticleQuantity);
							if (article.Quantity >= 0)
							{
								connection.Update(article);
							}
							else
							{
								connection.Rollback();
								throw new InsufficientQuantityException(orderArticle.ArticleQuantity, orderArticle.ArticleName, articleQuantityAvailable);
							}

						}
						else
						{
							//if orderArticle doesn't have lot, decrease its quantity only from table article
							Article article = connection.Table<Article>().Where(x => x.Id == orderArticle.ArticleId).FirstOrDefault();
							decimal quantityAvailable = article.Quantity;
							article.Quantity -= Convert.ToDecimal(orderArticle.ArticleQuantity);
							if (article.Quantity >= 0)
							{
								connection.Update(article);
							}
							else
							{
								connection.Rollback();
								throw new InsufficientQuantityException(orderArticle.ArticleQuantity, orderArticle.ArticleName, quantityAvailable);
							}

						}
					}
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}

				connection.Commit();
			
			}

		}
	}
}
