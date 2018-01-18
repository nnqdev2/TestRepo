using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using MvvmFoundation.Wpf;
using PropertyChanged;
using Commander;

namespace EpiPlanTool.ViewModels {

  using EpiPlanTool.AttachedProperties;
  using EpiPlanTool.Services;
  using Collections;
  using EpiPlanTool.Utilities;

  [ImplementPropertyChanged]
  public class BookedOrdersViewModel {
    #region Private Fields
    private readonly StatusMessageService _messageService;
    private readonly IAppRepository _repo;
    private readonly IBookedOrderViewModelFactory _factory;
    private ICommand _filterByValueCommand;
    private ICommand _filterByNonBlankCommand;
    private ICommand _clearAllFiltersCommand;
    private ICommand _filterByTextCommand;
    private ICommand _copyValueCommand;
    private List<FilterInfo> _filters;
    private PropertyInfo _tasksProperty;
    #endregion

    #region Constructors
    public BookedOrdersViewModel(
      IAppRepository repo,
      IBookedOrderViewModelFactory factory,
      StatusMessageService messageService
    ) {
      _repo = repo;
      _factory = factory;
      _messageService = messageService;
      _tasksProperty = typeof(BookedOrderViewModel).GetProperty("Tasks");
      Orders = new CollectionViewSource();
      _filters = new List<FilterInfo>();
      _filterByValueCommand = new RelayCommand(FilterByValue);
      _filterByNonBlankCommand = new RelayCommand(FilterByNonBlank);
      _clearAllFiltersCommand = new RelayCommand(ClearAllFilters);
      _filterByTextCommand = new RelayCommand<String>(FilterByText);
      _copyValueCommand = new RelayCommand<object>(CopyCellValue);
    }
    #endregion

    #region Private Methods
    private bool OrdersViewFilter(object item){
      bool result = true;
      try {
        foreach (var filter in _filters) {
          result = result && filter.Compare(item);
        }
      }
      catch (Exception ex) {
        Console.WriteLine("Filter error: {0}", ex.Message);
        result = false;
      }
      return result;
    }

    [OnCommand("OnSelectionChanged")]
    private void OnSelectionChanged() {
      SelectedProperty = null;
      FocusedTasks = null;
      if(SelectedColumn != null && SelectedOrder != null) {
        Binding binding = null;
        string propertyName = String.Empty;
        if(SelectedColumn is DataGridBoundColumn) {
          var col = SelectedColumn as DataGridBoundColumn;
          binding = (Binding)col.Binding;
          propertyName = binding.Path.Path;
        }
        if(SelectedColumn is DataGridTemplateColumn) {
          propertyName = AttachedProperties.AttachedStrings.GetPropertyName(SelectedColumn);
        }
        if(propertyName != String.Empty)
          SelectedProperty = SelectedOrder.GetType().GetProperty(propertyName);
        FocusedTasks = SelectedOrder.Tasks;
      }
    }

    private void OnSelectedColumnChanged() {
      OnSelectionChanged();
    }

    private void OnSelectedItemChanged() {
      SelectedOrder = SelectedItem as BookedOrderViewModel;
      OnSelectionChanged();
    }

    private FilterInfo createFilter(CompareFlags compareAs) {
      FilterInfo filterInfo = null;
      if (SelectedColumn != null && SelectedOrder != null) {
        if (SelectedColumn is DataGridBoundColumn) {
          var col = SelectedColumn as DataGridBoundColumn;
          Binding binding = (Binding)col.Binding;
          //filterInfo = new FilterInfo(SelectedOrder, binding.Path.Path, col);
          filterInfo = new FilterInfo(SelectedOrder, binding.Path.Path);
          filterInfo.CompareAs = compareAs;
          if (compareAs != CompareFlags.CompareNone)
            _filters.Add(filterInfo);
        }
        //hardcoded reactors property for now
        if (SelectedColumn is DataGridTemplateColumn) {
           //filterInfo = new FilterInfo(SelectedOrder, binding.Path.Path, col);
           filterInfo = new FilterInfo(SelectedOrder, "Reactors");
           filterInfo.CompareAs = compareAs;
           if (compareAs != CompareFlags.CompareNone)
             _filters.Add(filterInfo);
        }
      }
      return filterInfo;
    }

    private void RefreshFilter(){
      Orders.View.Refresh();
    }

    private void FilterByValue() {
      var filter = createFilter(CompareFlags.CompareEquals);
      filter.Value = filter.GetSourceValue();
      RefreshFilter();
    }

    private void FilterByNonBlank() {
      var filter = createFilter(CompareFlags.CompareNotBlank);
      RefreshFilter();
    }

    private void FilterByText(String value) {
       if (FilterText == null) {
          FilterByNonBlank();
          return;
       }
      var filter = createFilter(CompareFlags.CompareAsContainsString);
      if (filter != null) {
         filter.Value = FilterText;
         RefreshFilter();
      }
    }

    private void ClearAllFilters() {
      _filters.Clear();
      FilterText = String.Empty;
      RefreshFilter();
    }

    private void CopyCellValue(object value) {
      var filter = createFilter(CompareFlags.CompareNone);
      Clipboard.Clear();
      Clipboard.SetText(filter.GetSourceValue().ToString());
    }

    private void OnModelsChanged(){
      if (Orders.View != null) {
        Orders.View.Filter = null;
        Orders.Source = null;
      }
      Orders.Source = Models;
      Orders.View.Refresh();
      Orders.View.Filter = OrdersViewFilter;
      Orders.View.CurrentChanged += (s, e) => {
        SelectedOrder = Orders.View.CurrentItem as BookedOrderViewModel;
      };
    }
    #endregion

    #region Internal Methods
    internal void Load() {
      BookedOrderViewModelFactory.Clear();
      StatusMessageService.Message = "Loading booked orders...";
      Models =
        (from order in Repository.GetOrders()
        select _factory.Create(order)).ToList();
    }
    #endregion
    
    #region Public Properties
    public StatusMessageService StatusMessageService { get { return _messageService; } }
    public IAppRepository Repository { get { return _repo; } }
    public String FilterText { get; set; }
    public CollectionViewSource Orders { get; private set; }
    [AlsoNotifyFor("Orders")]
    public List<BookedOrderViewModel> Models { get; private set; }
    public object SelectedItem { get; set; }
    public BookedOrderViewModel SelectedOrder { get; private set; }
    public DataGridColumn SelectedColumn { get; set; }
    public PropertyInfo SelectedProperty { get; set; }
    public TaskCollection FocusedTasks { get; set; }
    public bool IsContextOpen { get; set; }
    #endregion

    #region Commands / Command Methods
    public ICommand FilterByValueCommand { get { return _filterByValueCommand; } }
    public ICommand FilterByNonBlanksCommand { get { return _filterByNonBlankCommand; } }
    public ICommand ClearAllFiltersCommand { get { return _clearAllFiltersCommand; } }
    public ICommand FilterByTextCommand { get { return _filterByTextCommand; } }
    public ICommand CopyValueCommand { get { return _copyValueCommand; } }
    #endregion
  }
}