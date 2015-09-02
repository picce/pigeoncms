#List of items

[Learn](/wiki/dev/items.md) how to use and extend items.

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
      var man = new PigeonCms.ItemsManager<Item, ItemsFilter>(/*checkUserContext*/true, /*writeMode*/false);
      var filter = new PigeonCms.ItemsFilter();
      filter.CategoryId = Acme.Settings.SampleCatId;
      var list = man.GetByFilter(filter, "");

      Rep1.DataSource = list;
      Rep1.DataBind();
  }
```
