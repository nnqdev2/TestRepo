using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Syntax;
using ExpressMapper;
using EpiPlanTool.Models;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Services {
  public interface IBookedOrderViewModelFactory {
    BookedOrderViewModel Create(BookedOrder order);
  }

  public interface IBookedOrderLookup {
    BookedOrderViewModel this[long index] {get; set;}
  }

  public class BookedOrderViewModelFactory : IBookedOrderViewModelFactory, IBookedOrderLookup {

    readonly IResolutionRoot resolutionRoot;
    static private Dictionary<long, BookedOrderViewModel> ViewModels =
       ViewModels = new Dictionary<long, BookedOrderViewModel>(500);

    public BookedOrderViewModelFactory(IResolutionRoot resolutionRoot){
      this.resolutionRoot = resolutionRoot;
    }

    public BookedOrderViewModel Create(BookedOrder order) {
      BookedOrderViewModel vm = this.resolutionRoot.Get<BookedOrderViewModel>();
      Mapper.Map<BookedOrder, BookedOrderViewModel>(order, vm);
      vm.Lots.AddRange(order.Lots);
      ViewModels.Add(order.BookedOrderID, vm);
      return vm;
    }

    BookedOrderViewModel IBookedOrderViewModelFactory.Create(BookedOrder order) {
      return this.Create(order);
    }

    BookedOrderViewModel IBookedOrderLookup.this[long index] {
      get {
        return 
          ViewModels.ContainsKey(index) ? ViewModels[index] : null; 
      }
      set { ViewModels[index] = value; }
    }

    public static void Clear(){
      ViewModels.Clear();
    }
  }
}
