using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UIBlazorFramework.Web.Components
{
    public partial class UISelect<TData>
    {
        [Parameter]
        public IList<TData> Data { get; set; }

        [Parameter]
        public TData SelectedItem { get; set; }

        [Parameter]
        public EventCallback<TData> SelectedItemChanged { get; set; }

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

        private IList<TData> _filterData;

        private string _heigthContainer;

        protected override void OnInitialized()
        {
            _filterData = Data;
            base.OnInitialized();
        }

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
            CalcHeightContainer();
        }

        private void OnFilter(ChangeEventArgs args)
        {
            _searchText = args.Value.ToString();
            if (!string.IsNullOrWhiteSpace(_searchText))
                _filterData = Data.Where(x => GetText(x).Contains(_searchText)).ToList();
            else
                _filterData = Data;

            CalcHeightContainer();
        }

        private void ClearSelected()
        {
            SelectedItem = default;
            SelectedItemChanged.InvokeAsync(default);
            _selectedItemText = null;

            Value = default;
            ValueChanged.InvokeAsync(default);
        }

        private void SelectItem(TData item)
        {
            SelectedItem = item;
            SelectedItemChanged.InvokeAsync(item);

            _selectedItemText = GetText(item);

            var value = GetValue(item);
            Value = value;
            ValueChanged.InvokeAsync(value);
            _showContainerOptions = false;
            _searchText = string.Empty;
            _filterData = Data;
        }

        private void CalcHeightContainer()
        {
            var height = (_filterData.Count * 38) + 50;
            _heigthContainer = height < 300 ? height + "px" : "300px";
        }
    }
}
