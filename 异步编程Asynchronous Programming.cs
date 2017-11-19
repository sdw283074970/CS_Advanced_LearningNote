//Q: 什么是异步编程？
//A: 异步编程(Asynchronous Programming)是C# 5.0加入的新特性。要理解异步编程，须要先知道异步执行模式(Synchronous Execution Model)和
  //同步执行模式(Synchronous Execution Model)。

//Q: 什么是同步执行模式？
//A: 同步执行指程序逐行执行命令/程序语句，一次只执行一句。当有函数/方法调用时，程序会等待该函数/方法执行完毕后再执行下一行。

//Q: 什么是异步执行？
//A: 与同步执行相反，异步执行逐步执行命令/语句，遇到需要调用函数/方法时不等待，直接执行下一行，直到所有语句执行完毕后，再调用函数。

//Q: 异步执行模式有什么好处？
//A: 在实际应用程序中，异步执行会显著提高应用UI的反应。如在Windows平台中的各种播放器，在载入播放文件的时候，我们仍然可以经行拖动窗口，改变其大小，点击
  //菜单等操作，这里就到了异步编程。
  
//Q: 什么时候要用到异步编程？
//A: 在任何需执行受阻的情况下我们都应该使用异步编程。如访问网页、访问文件系统、访问数据库、处理图片等情况。

//Q: 如何使用异步编程？
//A: 传统上有两种使用方法，第一种是使用多线程(Multi-threading)，第二种是使用回调函数(Callbacks)。用这两种方法来经行异步处理都很复杂，现在在5.0中
  //微软新加入了Async/Await模式来实现异步编程，被称作“基于任务的异步模式(Task-based Asynchronous Model)”。我们又通过关键词async和await来实现。
  
//Q: 具体如何使用？
//A: 举一个涉及到UI反应的例子。在WPF中，主要在.xaml中搭建UI，在.xmal.cs中编写程序。WPF是用来开发Windows桌面程序的程序，开局一个窗口，其他全靠自己
  //设计。如，假设我们已经在xaml中添加了一个button对象，这个按钮对象发布事件，后台订阅事件，实现“点击按钮即访问网页”的操作。原代码如下：
  
public partial calss MainWindow : Window  //WPF中的主窗口类，即主窗口的后台
{
  public MainWindow()
  {
    InitializeComponent();  //初始化主窗口
  }
  
  private void Button_Click(object sender, RoutedEventArgs e)   //订阅按钮的“点击”事件
  {
    DownloadHtml("http://mail.qq.com");   //按钮被点击后做出的回应
  }
  
  public void DownloadHtml(string url)    //定义一个下载函数，传入网址则下载其中的所有字符串，并按数组保存
  {
    var webClient = new WebClient();    //实例化网页客户端
    var html = webClient.DownloadString(url);   //保存下载的字符串数组
    
    using(var streamWriter = new StreamWriter(@"c:\project\result.html"))   //使用文件写入，在streamWriter消失后会在自动生成的finally释放空间
    {
      streamWriter.Write(html);   //写入文件
    }
  }
}







