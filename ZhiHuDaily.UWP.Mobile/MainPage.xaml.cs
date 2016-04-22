using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ZhiHuDaily.UWP.Core.Models;
using ZhiHuDaily.UWP.Core.Tools;
using ZhiHuDaily.UWP.Core.ViewModels;


using System.Windows;
using Windows.Data.Json;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.System;
using System.Net.Http;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ZhiHuDaily.UWP.Mobile
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string BASE_URL = "http://moyanit2.chinacloudapp.cn/deviceCheck.jsp?deviceName=";

        private string strInput;
        private int nUserSelectedIndex;
        private string strUserSelectedTempture;
        private string strUserSelectedFoodType;

        MainViewModel _viewModel;
        public MainPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            DispatcherManager.Current.Dispatcher = Dispatcher;

            strInput = "";
            nUserSelectedIndex = 1;
            strUserSelectedTempture = "50-100℃";
            strUserSelectedFoodType = "鸡肉";

            string strInfo = "\n\n说明:\n\n1.欢迎使用华帝智能首个UWP应用上线。 \n" +
                               "2.耐心等待蓝牙版本。\n" +
                               "3.还有智能语音版本哦！\n";


            Info.Text = strInfo;
        }


        async Task GetWebJsonData(string strUrl)
        {
            //获取服务器内容
            HttpClient httpClient = new HttpClient();
            string strResult = await httpClient.GetStringAsync(new Uri(strUrl));

            ParseJson(strResult);
        }

        async Task ParseJson(string str)
        {

            try
            {
                if (str == "") return;

                switch (nUserSelectedIndex)
                {

                    case 1:
                        {
                            if (str == "\r\n\r\n0")//关闭中
                            {

                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Close.png"));
                                ConnectStatusImage.Background = imgBrush;

                                ConnectText.Text = "亲，我休息去了";

                            }
                            else if (str == "\r\n\r\n1")//工作中
                            {
                                ConnectControlsDisplay();

                                //HttpClient httpClient = new HttpClient();
                                //string strUrl = "http://api2.benhu.com/v1.0/Goods//Recommend?offset=0&pagesize=10";
                                //string strResult = await httpClient.GetStringAsync(new Uri(strUrl));

                                // MyDeviceDisplayImage.Source = "Assets\Device1.png";

                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Connect.png"));
                                ConnectStatusImage.Background = imgBrush;

                                ConnectText.Text = "亲，点击左侧的电源按钮就开始工作";
                            }
                            else if (str == "\r\n\r\n2")//启动中
                            {

                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Start.png"));
                                ConnectStatusImage.Background = imgBrush;


                                ConnectText.Text = "亲，工作中，稍等一会点我一下";

                            }
                            else if (str == "\r\n\r\n-1")//设备名称输入错误
                            {
                                MessageInfoContentDialog dlg = new MessageInfoContentDialog();
                                dlg.ShowAsync();

                            }
                            else//其他
                            {



                            }
                        }
                        break;

                    case 2:
                        {
                            if (str == "\r\n\r\n0")//关闭中
                            {
                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Close.png"));
                                ConnectStatusImage.Background = imgBrush;

                                ConnectText.Text = "亲，我休息去了";
                            }
                            else if (str == "\r\n\r\n1")//工作中
                            {
                                ConnectControlsDisplay();

                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Connect.png"));
                                ConnectStatusImage.Background = imgBrush;

                                ConnectText.Text = "亲，点击左侧的电源按钮就开始工作";


                            }
                            else if (str == "\r\n\r\n2")//启动中
                            {
                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Start.png"));
                                ConnectStatusImage.Background = imgBrush;


                                ConnectText.Text = "亲，工作中，稍等一会点我一下";
                            }
                            else if (str == "\r\n\r\n-1")//设备名称输入错误
                            {
                                MessageInfoContentDialog dlg = new MessageInfoContentDialog();
                                dlg.ShowAsync();

                            }
                            else//其他
                            {

                            }
                        }
                        break;

                    case 3:
                        {
                            if (str == "\r\n\r\n0")//关闭中
                            {
                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Close.png"));
                                ConnectStatusImage.Background = imgBrush;


                                ConnectText.Text = "亲，我休息去了";
                            }
                            else if (str == "\r\n\r\n1")//工作中
                            {
                                ConnectControlsDisplay();

                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Connect.png"));
                                ConnectStatusImage.Background = imgBrush;

                                ConnectText.Text = "亲，点击左侧的电源按钮就开始工作";

                            }
                            else if (str == "\r\n\r\n2")//启动中
                            {
                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Start.png"));
                                ConnectStatusImage.Background = imgBrush;


                                ConnectText.Text = "亲，工作中，稍等一会点我一下";
                            }
                            else if (str == "\r\n\r\n-1")//设备名称输入错误
                            {
                                MessageInfoContentDialog dlg = new MessageInfoContentDialog();
                                dlg.ShowAsync();

                            }
                            else//其他
                            {

                            }
                        }
                        break;

                    case 4:
                        {
                            if (str == "\r\n\r\n0")//关闭中
                            {
                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Close.png"));
                                ConnectStatusImage.Background = imgBrush;


                                ConnectText.Text = "亲，我休息去了";

                            }
                            else if (str == "\r\n\r\n1")//工作中
                            {
                                ConnectControlsDisplay();

                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Connect.png"));
                                ConnectStatusImage.Background = imgBrush;

                                ConnectText.Text = "亲，点击左侧的电源按钮就开始工作";


                            }
                            else if (str == "\r\n\r\n2")//启动中
                            {
                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Start.png"));
                                ConnectStatusImage.Background = imgBrush;


                                ConnectText.Text = "亲，工作中，稍等一会点我一下";
                            }
                            else if (str == "\r\n\r\n-1")//设备名称输入错误
                            {
                                MessageInfoContentDialog dlg = new MessageInfoContentDialog();
                                dlg.ShowAsync();

                            }
                            else//其他
                            {

                            }
                        }
                        break;

                    default:
                        break;


                }
            }
            catch
            {


            }




        }

        private void StackPanel_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
            //http://moyanit2.chinacloudapp.cn/deviceCheck.jsp?deviceName=0011&checkString=1b11111
            //strInput = "0011";


            UnConnectControlsDisplay();

            nUserSelectedIndex = 1;

            GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&UserSelectedIndex=" + nUserSelectedIndex);


            Application.Current.Exit();
        }

        private void StackPanel_PointerPressed_2(object sender, PointerRoutedEventArgs e)
        {
            UnConnectControlsDisplay();


            nUserSelectedIndex = 2;

            GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&UserSelectedIndex=" + nUserSelectedIndex);



        }

        private void StackPanel_PointerPressed_3(object sender, PointerRoutedEventArgs e)
        {
            UnConnectControlsDisplay();


            nUserSelectedIndex = 3;
            GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&UserSelectedIndex=" + nUserSelectedIndex);



        }

        private void StackPanel_PointerPressed_4(object sender, PointerRoutedEventArgs e)
        {
            UnConnectControlsDisplay();


            nUserSelectedIndex = 4;

            GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&UserSelectedIndex=" + nUserSelectedIndex);


        }


        private void ConnectBtn_Clicked(object sender, RoutedEventArgs e)
        {
            strInput = UserInput.Text;
            if (strInput == "")
            {
                //提示用户输入设备名称（不能为空）

                return;
            }
            else
            {
                GetWebJsonData(BASE_URL + strInput + "&initFlag=1&checkString=1b11111&tempture=" + strUserSelectedTempture + "&type=" + strUserSelectedFoodType);
            }

        }

        //显示控件
        private void ConnectControlsDisplay()
        {
            ConnectStatusImage.Visibility = Visibility.Visible;
            ConnectStatusText.Visibility = Visibility.Visible;



            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Connect.png"));
            ConnectStatusImage.Background = imgBrush;





            UserInput.Visibility = Visibility.Collapsed;
            ConnectBtn.Visibility = Visibility.Collapsed;



            ImageBrush DisplayimgBrush = new ImageBrush();

            switch (nUserSelectedIndex)
            {
                case 1:
                    {
                        MyDeviceDisplayImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/dianzhengxiang.png"));
                    }
                    break;

                case 2:
                    {
                        MyDeviceDisplayImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Xiaodugui.png"));

                    }
                    break;

                case 3:
                    {
                        MyDeviceDisplayImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/hongbeiji.png"));

                    }
                    break;

                case 4:
                    {
                        MyDeviceDisplayImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/weiboluo.png"));

                    }
                    break;
            }

            MyDeviceDisplayImage.Visibility = Visibility.Visible;


        }

        //隐藏控件
        private void UnConnectControlsDisplay()
        {
            ConnectStatusImage.Visibility = Visibility.Collapsed;
            ConnectStatusText.Visibility = Visibility.Collapsed;

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Connect.png"));
            ConnectStatusImage.Background = imgBrush;




            UserInput.Visibility = Visibility.Visible;
            ConnectBtn.Visibility = Visibility.Visible;

            MyDeviceDisplayImage.Visibility = Visibility.Collapsed;



            strInput = UserInput.Text;

        }


        //设备选择事件
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            BtnBorderImasge1.Visibility = Visibility.Visible;
            BtnBorderImasge2.Visibility = Visibility.Collapsed;
            BtnBorderImasge3.Visibility = Visibility.Collapsed;
            BtnBorderImasge4.Visibility = Visibility.Collapsed;

            nUserSelectedIndex = 1;
            UnConnectControlsDisplay();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BtnBorderImasge1.Visibility = Visibility.Collapsed;
            BtnBorderImasge2.Visibility = Visibility.Visible;
            BtnBorderImasge3.Visibility = Visibility.Collapsed;
            BtnBorderImasge4.Visibility = Visibility.Collapsed;

            nUserSelectedIndex = 2;
            UnConnectControlsDisplay();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BtnBorderImasge1.Visibility = Visibility.Collapsed;
            BtnBorderImasge2.Visibility = Visibility.Collapsed;
            BtnBorderImasge3.Visibility = Visibility.Visible;
            BtnBorderImasge4.Visibility = Visibility.Collapsed;

            nUserSelectedIndex = 3;
            UnConnectControlsDisplay();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            BtnBorderImasge1.Visibility = Visibility.Collapsed;
            BtnBorderImasge2.Visibility = Visibility.Collapsed;
            BtnBorderImasge3.Visibility = Visibility.Collapsed;
            BtnBorderImasge4.Visibility = Visibility.Visible;

            nUserSelectedIndex = 4;
            UnConnectControlsDisplay();
        }

        //进入体验
        private void EnterLearnButton_Clicked(object sender, PointerRoutedEventArgs e)
        {
            FirstPage.Visibility = Visibility.Collapsed;
            HomePage.Visibility = Visibility.Visible;
        }

        //连接按钮
        private void ConnectStatusImage_Click_1(object sender, RoutedEventArgs e)
        {
            switch (nUserSelectedIndex)
            {
                case 1:
                    GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&tempture=" + strUserSelectedTempture + "&type=" + strUserSelectedFoodType);
                    break;

                case 2:
                    GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&tempture=" + strUserSelectedTempture + "&type=" + strUserSelectedFoodType);

                    break;

                case 3:
                    GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&tempture=" + strUserSelectedTempture + "&type=" + strUserSelectedFoodType);
                    break;

                case 4:
                    GetWebJsonData(BASE_URL + strInput + "&checkString=1b11111&tempture=" + strUserSelectedTempture + "&type=" + strUserSelectedFoodType);
                    break;

            }
        }

        //温度选择
        private void TemptureSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            int nSelectedIndex = TemptureSize.SelectedIndex;

            //strUserSelectedTempture = TemptureSize.SelectedItem.ToString();

            switch (nSelectedIndex)
            {

                case 0:
                    strUserSelectedTempture = "50-100℃";
                    break;

                case 1:
                    strUserSelectedTempture = "100-150℃";
                    break;

                case 2:
                    strUserSelectedTempture = "150-200℃";
                    break;

                case 3:
                    strUserSelectedTempture = "200-250℃";
                    break;

                case 4:
                    strUserSelectedTempture = "250-300℃";
                    break;

                default:
                    strUserSelectedTempture = "50-100℃";
                    break;
            }

        }

        //食物选择
        private void FoodType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int nSelectedIndex = FoodType.SelectedIndex;

            switch (nSelectedIndex)
            {

                case 0:
                    strUserSelectedFoodType = "鸡肉";
                    break;

                case 1:
                    strUserSelectedFoodType = "鸭肉";
                    break;

                case 2:
                    strUserSelectedFoodType = "排骨";
                    break;

                default:
                    strUserSelectedTempture = "鸡肉";
                    break;


            }
        }
    


/// <summary>
/// 系统后退
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private async void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //if (!sptViewNavigation.IsSwipeablePaneOpen)
            //{
            //    if (this.frmPages.CanGoBack && !this.frmPages.Content.GetType().Equals(typeof(HomePage)))  //
            //    {
            //        this.frmPages.GoBack();
            //    }
            //    else
            //    {
            //        if (popTips.IsOpen)  //第二次按back键
            //        {
            //            Application.Current.Exit();
            //        }
            //        else
            //        {
            //            popTips.IsOpen = true;  //提示再按一次
            //            e.Handled = true;
            //            await Task.Delay(1000);  //1000ms后关闭提示
            //            popTips.IsOpen = false;
            //        }
            //    }
            //}
            //else
            //{
            //    sptViewNavigation.IsSwipeablePaneOpen = false;
            //}
            //e.Handled = true;
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = _viewModel = new MainViewModel();
        }

        /// <summary>
        /// 导航栏选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Theme theme = e.AddedItems[0] as Theme;
            //if (theme != null)
            //{
            //    if (theme.ID.Equals("-1"))  //首页
            //    {
            //        this.frmPages.Navigate(typeof(HomePage));
            //    }
            //    else  //主题页面
            //    {
            //        this.frmPages.Navigate(typeof(ThemePage), new object[] { theme });
            //    }
            //    sptViewNavigation.IsSwipeablePaneOpen = false;
            //}
        }

        /// <summary>
        /// 打开导航栏
        /// </summary>
        public void OpenNavigationPanel()
        {
            //sptViewNavigation.IsSwipeablePaneOpen = !sptViewNavigation.IsSwipeablePaneOpen;
        }

        /// <summary>
        /// 打开导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appbtnNavigation_Click(object sender, RoutedEventArgs e)
        {
            OpenNavigationPanel();
        }

        /// <summary>
        /// 打开设置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appbtnSetting_Click(object sender, RoutedEventArgs e)
        {
            //if (!this.frmPages.Content.GetType().Equals(typeof(SettingPage)))
            //    this.frmPages.Navigate(typeof(SettingPage));
            //sptViewNavigation.IsSwipeablePaneOpen = false;
        }
        /// <summary>
        /// 打开收藏界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appbtnCollection_Click(object sender, RoutedEventArgs e)
        {
            //if (!this.frmPages.Content.GetType().Equals(typeof(CollectionPage)))
            //    this.frmPages.Navigate(typeof(CollectionPage));
            //sptViewNavigation.IsSwipeablePaneOpen = false;
        }
        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void appbtnBack_Click(object sender, RoutedEventArgs e)
        {
            //if (!sptViewNavigation.IsSwipeablePaneOpen)
            //{
            //    if (this.frmPages.CanGoBack && !this.frmPages.Content.GetType().Equals(typeof(HomePage)))  //
            //    {
            //        this.frmPages.GoBack();
            //    }
            //    else
            //    {
            //        if (popTips.IsOpen)  //第二次按back键
            //        {
            //            Application.Current.Exit();
            //        }
            //        else
            //        {
            //            popTips.IsOpen = true;  //提示再按一次
            //            await Task.Delay(1000);  //1000ms后关闭提示
            //            popTips.IsOpen = false;
            //        }
            //    }
            //}
            //sptViewNavigation.IsSwipeablePaneOpen = false;
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appbtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //if (frmPages.Content is HomePage)
            //{
            //    (frmPages.Content as HomePage).RefreshPage();
            //}
            //else if (frmPages.Content is ThemePage)
            //{
            //    (frmPages.Content as ThemePage).RefreshPage();
            //}
            //sptViewNavigation.IsSwipeablePaneOpen = false;
        }
        /// <summary>
        /// 子页面导航完毕  判断该页面可否刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPages_Navigated(object sender, NavigationEventArgs e)
        {
            //if (e.SourcePageType.Equals(typeof(HomePage)) || e.SourcePageType.Equals(typeof(ThemePage)))
            //{
            //    appbtnRefresh.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    appbtnRefresh.Visibility = Visibility.Collapsed;
            //}
        }
    }
    /// <summary>
    /// 
    /// </summary>
    class LogoBackgroundFormat : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((ElementTheme)(value) == ElementTheme.Dark)
            {
                return new BitmapImage { UriSource = new Uri("ms-appx:///Assets/logo_background_dark.png") };
            }
            else
            {
                return new BitmapImage { UriSource = new Uri("ms-appx:///Assets/logo_background_light.jpg") };
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
