package md5ff5d8105fff58aa239a672c86c908793;


public class CustomerViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MobileOrder.RecyclerViewHelperClasses.CustomerViewHolder, MobileOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomerViewHolder.class, __md_methods);
	}


	public CustomerViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == CustomerViewHolder.class)
			mono.android.TypeManager.Activate ("MobileOrder.RecyclerViewHelperClasses.CustomerViewHolder, MobileOrder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
