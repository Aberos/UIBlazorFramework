namespace UIBlazorFramework.Web.Components
{
    public partial class UISelect
    {
        private bool _showContainerOptions;

        private string _selectedItemText = "Teste";

        private string _searchText;

        private void OnClickSpan()
        {
            _showContainerOptions = !_showContainerOptions;
        }
    }
}
