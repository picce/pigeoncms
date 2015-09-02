#List of items

**Items** are the *bricks* of any content in Pigeon.
`PigeonCms.Item` class has many useful properties and methods ready to use and to extend to meet your specific Business Logic needs.

The main features of **Items** are:
* localized contents
* images and files attachments
* read/write permissions for specific user roles

For example **Items** could be used to represent:
* static page contents
* blog posts
* news
* shop products
* ..and so on..

Example using *<asp:Repeater>*
```C#
  protected void Page_Load(object sender, EventArgs e)
  {
      loadList();
  }

  //repeater ItemDataBound
  protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
  {
      if (e.Item.ItemType == ListItemType.Header)
          return;

      var item = (Item)e.Item.DataItem;

      var LitTitle = (Literal)e.Item.FindControl("LitTitle");
      LitTitle.Text = item.Title;

      var LitImg = (Literal)e.Item.FindControl("LitImg");
      LitImg.Text = "<img src='" + item.DefaultImage.FileUrl + "' width='50' />";

      var LitPermissions = (Literal)e.Item.FindControl("LitPermissions");
      LitPermissions.Text = item.ReadAccessType.ToString();
  }

  private void loadList()
  {
      //first parameter means to check userContext
      var man = new PigeonCms.ItemsManager<Item, ItemsFilter>(true, false);
      var filter = new PigeonCms.ItemsFilter();
      filter.CategoryId = Acme.Settings.SampleCatId;
      var list = man.GetByFilter(filter, "");

      Rep1.DataSource = list;
      Rep1.DataBind();
  }
```
