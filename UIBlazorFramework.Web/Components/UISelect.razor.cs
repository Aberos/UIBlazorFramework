using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UIBlazorFramework.Web.Components
{
    public partial class UISelect<TData>
    {
        [Parameter]
        public IList<TData> Data { get; set; }

        [Parameter]
        public TData SeletedItem { get; set; }

        [Parameter]
        public EventCallback<TData> SeletedItemChanged { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<TData, object>> ValueField { get; set; }

        [Parameter]
        public Expression<Func<TData, object>> TextField { get; set; }

        private bool _showContainerOptions;

        private string _selectedItemText;

        private string _searchText;

        private string GetText(TData item)
        {
            if (item == null)
                return default;

            var compiledTextField = TextField.Compile();
            var textFromItem = compiledTextField.Invoke(item);

            return textFromItem.ToString();
        }

        private string GetValue(TData item)
        {
            if (item == null)
                return default;

            var compiledValueField = ValueField.Compile();
            var valueFromItem = compiledValueField.Invoke(item);

            return valueFromItem.ToString();
        }

        private void BindContainerOptions()
        {
            _showContainerOptions = !_showContainerOptions;
        }

        private void SelectItem(TData item)
        {
            SeletedItem = item;
            SeletedItemChanged.InvokeAsync(item);

            _selectedItemText = GetText(item);

            var value = GetValue(item);
            Value = value;
            ValueChanged.InvokeAsync(value);
            _showContainerOptions = false;
            _searchText = string.Empty;
        }
    }
}
