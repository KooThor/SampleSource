using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleSource.Domain
{
    public class ListsAndGridsViewModel : ViewModelBase
    {
        public ListsAndGridsViewModel()
        {
            Items1 = CreateData_Day();
            Items2 = CreateData_Date();
            Items3 = CreateData_Day();

            foreach (var model in Items3)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(SelectableViewModel.IsSelected))
                        OnPropertyChanged(nameof(IsAllItems1Selected));
                };
            }
        }

        public bool? IsAllItems1Selected
        {
            get
            {
                var selected = Items3.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : (bool?) null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items3);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, IEnumerable<SelectableViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private static ObservableCollection<SelectableViewModel> CreateData_Day()
        {
            ObservableCollection<SelectableViewModel> rtn = new ObservableCollection<SelectableViewModel>();
            // 初期曜日取得：日曜日
            DateTime theDay = new DateTime(2021, 5, 9);
            for (int i = 0; i < 7; i++)
            {
                DateTime getDay = theDay.AddDays(i);
                SelectableViewModel v = new SelectableViewModel();
                v.Code = char.Parse(getDay.ToString("ddd", System.Globalization.CultureInfo.GetCultureInfo("ja-JP")));
                v.Name = "chk" + (i+1).ToString();
                v.Description = getDay.ToString("dddd", System.Globalization.CultureInfo.GetCultureInfo("ja-JP"));
                rtn.Add(v);
            }
            return rtn;
        }

        private static ObservableCollection<SelectableViewModel> CreateData_Date()
        {
            ObservableCollection<SelectableViewModel> rtn = new ObservableCollection<SelectableViewModel>();
            char[] suji = { '①', '②', '③', '④', '⑤', '⑥', '⑦', '⑧', '⑨', '⑩', '⑪', '⑫', '⑬', '⑭', '⑮', '⑯', '⑰', '⑱', '⑲', '⑳', '㉑', '㉒', '㉓', '㉔', '㉕', '㉖', '㉗', '㉘', '㉙', '㉚', '㉛' };
            for (int i = 0; i < 31; i++)
            {
                SelectableViewModel v = new SelectableViewModel();
                v.Code = suji[i];
                v.Name = "chk" + (i + 1).ToString();
                v.Description = (i + 1).ToString() + "日";
                rtn.Add(v);
            }
            return rtn;
        }

        public ObservableCollection<SelectableViewModel> Items1 { get; }
        public ObservableCollection<SelectableViewModel> Items2 { get; }
        public ObservableCollection<SelectableViewModel> Items3 { get; }
        public string ShowSet1 { get; set; }
        public string ShowSet2 { get; set; }

        public IEnumerable<string> Foods => new[] { "Burger", "Fries", "Shake", "Lettuce" };
    }
}