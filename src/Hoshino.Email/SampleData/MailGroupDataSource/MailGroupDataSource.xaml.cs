//   *********  请勿修改此文件   *********
//   此文件由设计工具再生成。更改
//   此文件可能会导致错误。
namespace Expression.Blend.SampleData.MailGroupDataSource
{
    using System; 
    using System.ComponentModel;

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
    internal class MailGroupDataSource { }
#else

    public class MailGroupDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MailGroupDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("ms-appx:/SampleData/MailGroupDataSource/MailGroupDataSource.xaml", UriKind.RelativeOrAbsolute);
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

        private string _EmailBccAccountName = string.Empty;

        public string EmailBccAccountName
        {
            get
            {
                return this._EmailBccAccountName;
            }

            set
            {
                if (this._EmailBccAccountName != value)
                {
                    this._EmailBccAccountName = value;
                    this.OnPropertyChanged("EmailBccAccountName");
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

        private string _EmailBccAccountCreateTime = string.Empty;

        public string EmailBccAccountCreateTime
        {
            get
            {
                return this._EmailBccAccountCreateTime;
            }

            set
            {
                if (this._EmailBccAccountCreateTime != value)
                {
                    this._EmailBccAccountCreateTime = value;
                    this.OnPropertyChanged("EmailBccAccountCreateTime");
                }
            }
        }
    }
#endif
}
