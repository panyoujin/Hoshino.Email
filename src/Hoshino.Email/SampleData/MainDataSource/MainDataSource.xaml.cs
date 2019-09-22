//   *********  请勿修改此文件   *********
//   此文件由设计工具再生成。更改
//   此文件可能会导致错误。
namespace Expression.Blend.SampleData.MainDataSource
{
    using System; 
    using System.ComponentModel;

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
    internal class MainDataSource { }
#else

    public class MainDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("ms-appx:/SampleData/MainDataSource/MainDataSource.xaml", UriKind.RelativeOrAbsolute);
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

    public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
    { 
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

        private double _EmailAccountSpace = 0;

        public double EmailAccountSpace
        {
            get
            {
                return this._EmailAccountSpace;
            }

            set
            {
                if (this._EmailAccountSpace != value)
                {
                    this._EmailAccountSpace = value;
                    this.OnPropertyChanged("EmailAccountSpace");
                }
            }
        }

        private double _EmailAccountMaxEmailCount = 0;

        public double EmailAccountMaxEmailCount
        {
            get
            {
                return this._EmailAccountMaxEmailCount;
            }

            set
            {
                if (this._EmailAccountMaxEmailCount != value)
                {
                    this._EmailAccountMaxEmailCount = value;
                    this.OnPropertyChanged("EmailAccountMaxEmailCount");
                }
            }
        }

        private string _EmailAccountCategoryName = string.Empty;

        public string EmailAccountCategoryName
        {
            get
            {
                return this._EmailAccountCategoryName;
            }

            set
            {
                if (this._EmailAccountCategoryName != value)
                {
                    this._EmailAccountCategoryName = value;
                    this.OnPropertyChanged("EmailAccountCategoryName");
                }
            }
        }

        private double _EmailAccountRemainEmailCount = 0;

        public double EmailAccountRemainEmailCount
        {
            get
            {
                return this._EmailAccountRemainEmailCount;
            }

            set
            {
                if (this._EmailAccountRemainEmailCount != value)
                {
                    this._EmailAccountRemainEmailCount = value;
                    this.OnPropertyChanged("EmailAccountRemainEmailCount");
                }
            }
        }

        private string _EmailAccountAddress = string.Empty;

        public string EmailAccountAddress
        {
            get
            {
                return this._EmailAccountAddress;
            }

            set
            {
                if (this._EmailAccountAddress != value)
                {
                    this._EmailAccountAddress = value;
                    this.OnPropertyChanged("EmailAccountAddress");
                }
            }
        }

        private string _EmailAccountNextSendTime = string.Empty;

        public string EmailAccountNextSendTime
        {
            get
            {
                return this._EmailAccountNextSendTime;
            }

            set
            {
                if (this._EmailAccountNextSendTime != value)
                {
                    this._EmailAccountNextSendTime = value;
                    this.OnPropertyChanged("EmailAccountNextSendTime");
                }
            }
        }
    }
#endif
}
