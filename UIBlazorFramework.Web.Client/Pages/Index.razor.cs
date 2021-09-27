using System.Collections.Generic;
using UIBlazorFramework.Model.Entities;

namespace UIBlazorFramework.Web.Client.Pages
{
    public partial class Index
    {
        public List<OptionItem> SelectData { get; set; }

        public OptionItem SelectedItem { get; set; }

        protected override void OnInitialized()
        {
            SelectData = new List<OptionItem>();
            for(var i = 1; i <= 20; i++)
            {
                SelectData.Add(new OptionItem()
                {
                    Value = i,
                    Text = "Option " + i
                });
            }
            base.OnInitialized();
        }
    }
}
