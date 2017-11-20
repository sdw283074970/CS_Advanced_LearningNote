//Q: 什么是异步编程？
//A: 异步编程(Asynchronous Programming)是C# 5.0加入的新特性。要理解异步编程，须要先知道异步执行模式(Synchronous Execution Model)和
  //同步执行模式(Synchronous Execution Model)。

//Q: 什么是同步执行模式？
//A: 同步执行指程序逐行执行命令/程序语句，一次只执行一句。当有函数/方法调用时，程序会等待该函数/方法执行完毕后再执行下一行。

//Q: 什么是异步执行？
//A: 与同步执行不同，异步执行逐步执行命令/语句，遇到调用函数/方法时不等待其执行完毕，直接执行下一行。

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
    InitializeComponent();  //初始化桌面程序主窗口
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

//在这个后台文件中，会产生时间阻碍的语句为:

    var html = webClient.DownloadString(url);
    //以及
    streamWriter.Write(html);

//以上程序执行到这一步时，会暂停等待，直到这一步完成才执行接下来的using语句，同时在点击按钮后主窗口会一直无法拖动直到下载完成。
//我们可以用async/await模式来提高程序的表现。因为产生的阻碍都在DownloadHtml方法中，所以我们需要对这个方法进行改造。新代码如下：

  public async Task DownloadHtmlAsync(string url)   //使用async/await模式的异步操作在名称后面加上Async，这是共识
  {
    var webClient = new WebClient();    //不会产生阻碍的语句不用变动
    //在5.0后很多会产生阻碍的方法都有了无阻碍版本，认准所有后缀为TaskAsync的方法(没有才用后缀为Async的方法)
    var html = await webClient.DownloadStringTaskAsync(url);   //调用任何返回Task<T>类型的方法都要配上await修饰符
    using(var streamWriter = new StreamWriter(@"c:\project\result.html"))
    {
      await streamWriter.WriteAsync(html);   //同上
    }
  }

//返回类型需要改成Task类。Task是一个封装了异步操作状态的对象，如果本来的返回对象是非void类型，那么这里就需要使用Task的泛型，如Task<string>。
//await修饰符仅仅是一个标记，用来告诉编译器这一句需要占用大量资源/时间。这样编译器就会直接把控制权交还给标记方法的调用者，而不是继续阻碍线程。

//Q: 在实例中，async/await模式的执行顺序是啥？
//A: 编译器会认出哪些方法有async修饰符，方法中有哪些语句有await修饰符，然后编译器会遵循以下步骤：
  //1.依照await语句的多少产生数个回调函数分割方法中的语句，在此例中有一个await语句，产生一个回调函数将DownloadHtmlAsync方法以await为界分为两部分；
  //2.执行第一部分，即到有await标记的语句为止，然后编译器将控制权交还给标记await方法的调用者，解放线程；
  //3.当有await标记的语句执行结束，编译器收到回调函数通知，然后就回到这一带await标记的语句，并执行下一句，这一回调过程完全交由编译器执行。

//Q: 当我们把一个方法的返回类型改为Task<T>后，如何才能复原其返回结果好让其他方法能够正常调用？
//A: 在调用这个方法的地方加上await前缀即可。注意也要给调用这个方法的调用者加上async修饰符。如：


private async void Button_Click(object sender, RoutedEventArgs e)   //await必须跟随async配套使用
  {
    //假设DownloadHtml返回的是string，现使用async/await模式致其返回类型变成Task<string>
    var html = await DownloadHtmlTaskAsync("http://mail.qq.com");   //加上await修饰符即可正常访问string方法
    MessageBox.Show(html.Substring(0,1));   //string类的Substring方法就可以正常调用
  }

//我们也可以不用直接给调用的方法加上await，取而代之先保存其返回的任务封装状态，然后对这个任务状态标记await，代码如下：

private async void Button_Click(object sender, RoutedEventArgs e) 
  {
    var htmlTask = DownloadHtmlTaskAsync("http://mail.qq.com");   //将异步执行任务保存成为任务状态，即Task<string>类型
    var html = await htmlTask;    //对任务状态标记
    MessageBox.Show(html.Substring(0,1));   //string类的Substring方法仍然可以正常调用
  }

//这么写的目的是为了更加解释清楚线程控制权的变化。当方法块执行第一句，线程执行到在htmlTask赋值，然后立刻执行第二句；第二句执行时线程返回到
  //Button_Click，即UI仍然能正常操作；当第二句执行完毕后，控制权才交给第三句并执行MessageBox语句。

//暂时想到这么多，最后更新2017/11/19
