//   *********  请勿修改此文件   *********
//   此文件由设计工具再生成。更改
//   此文件可能会导致错误。
namespace Expression.Blend.SampleData.EmailInfoDataSource
{
    using System; 
    using System.ComponentModel;

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
    internal class EmailInfoDataSource { }
#else

    public class EmailInfoDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public EmailInfoDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("ms-appx:/SampleData/EmailInfoDataSource/EmailInfoDataSource.xaml", UriKind.RelativeOrAbsolute);
                System.Windows.Application.LoadComponent(this, resourceUri);
            }
            catch
            {
            }
        }

        private ItemCollection _Collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }
    }

    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _EmailTitle = string.Empty;

        public string EmailTitle
        {
            get
            {
                return this._EmailTitle;
            }

            set
            {
                if (this._EmailTitle != value)
                {
                    this._EmailTitle = value;
                    this.OnPropertyChanged("EmailTitle");
                }
            }
        }

        private double _TotalQty = 0;

        public double TotalQty
        {
            get
            {
                return this._TotalQty;
            }

            set
            {
                if (this._TotalQty != value)
                {
                    this._TotalQty = value;
                    this.OnPropertyChanged("TotalQty");
                }
            }
        }

        private double _AlreadySentQty = 0;

        public double AlreadySentQty
        {
            get
            {
                return this._AlreadySentQty;
            }

            set
            {
                if (this._AlreadySentQty != value)
                {
                    this._AlreadySentQty = value;
                    this.OnPropertyChanged("AlreadySentQty");
                }
            }
        }

        private double _NeedSentQty = 0;

        public double NeedSentQty
        {
            get
            {
                return this._NeedSentQty;
            }

            set
            {
                if (this._NeedSentQty != value)
                {
                    this._NeedSentQty = value;
                    this.OnPropertyChanged("NeedSentQty");
                }
            }
        }

        private double _FailQty = 0;

        public double FailQty
        {
            get
            {
                return this._FailQty;
            }

            set
            {
                if (this._FailQty != value)
                {
                    this._FailQty = value;
                    this.OnPropertyChanged("FailQty");
                }
            }
        }

        private string _EmailState = string.Empty;

        public string EmailState
        {
            get
            {
                return this._EmailState;
            }

            set
            {
                if (this._EmailState != value)
                {
                    this._EmailState = value;
                    this.OnPropertyChanged("EmailState");
                }
            }
        }
    }

    public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
    { 
    }
#endif
}
