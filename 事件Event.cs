//写在前面：本篇中的“编码(Encode)”一词指例子中的对视频进行编码(VideoEncode)，与主题事件(Event)和程序本身无关，仅仅是例子中的例子。

//Q: 什么是事件？事件有什么用？
//A: 事件(Event)是对象之间沟通交流的机制，被用来构造松耦合程序结构和拓展功能。
  //如一个对象VideoEncoder在编码完成后另一个对象MailService就会知道并发送编码完成的Email，这个过程就需要用到事件来实现。

//Q: 事件是怎么工作的？原理是什么？
//A: 构建一个事件需要用到发布(Publish) - 订阅(Subscribe)体系。即一个对象发布事件，其他任何对这个事件感兴趣的对象订阅这个事件并做出相应反应。
  //在以上例子中，VideoEncoder就是事件发布者，MailService就是订阅者之一，“编码完成”就是事件，“编码完成发送邮件”就是事件出发后订阅者做出的反应。
  //如何形成这个体系？这里就需要一个类似协议的东西，只要发布者订阅者双方签署了这个协议，那么订阅者就会对事件的触发做出反应。

//Q: 那么这个“协议”如何签署？
//A: 这个“协议”就是委托，通过委托感知订阅者的eventHandler方法中的签名。当事件发布者触发事件，为了通知订阅者，我们需要一个返回类型为void的方法，
  //方法签名必须与发布者的委托签名相同，即相同的参数传递进去，最后将订阅者的回应方法添加到发布者的事件委托列表中，则完成签署这个“协议”。
  
//Q: 发布者如何发布事件？订阅者又如何订阅事件？
//A: 发布者发布事件只需跟随三步走。
  //1. 定义一个委托；
  //2. 定义一个基于委托的事件；
  //3. 发布事件。
  //订阅者订阅事件则只用定义一个方法签名与发布者委托相同的void方法即可。以视频编码器为例，使用事件之前的状态为：
  
class Video
{
  public string title { get; set;}
}//定义一个video类

class VideoEncoder
{
  public void Encode (Video video)
  {
    Console.WriteLine("Start encoding...");
    Thread.Sleep(3000);   //模拟编码过程消耗的时间3秒
  }
}//视频编码类，及其编码方法

//当执行VideoEncoder中的编码方法3秒后，视频编码完毕。现我们加入一个通知功能，即编码完成后通过邮件寄送编码完成的通知。实现这个功能就需要用到事件。
//Video类不变，VideoEncoder类自然就是事件发布者，跟随发布事件三步骤我们可以有：

class VideoEncoder
{
  //关于命名标准，因为事件为编码完成后的事件，所以前半部分命名为VideoEncoded，后部分为统一标准EventHandler表明这是事件发布委托。
  //一般情况下除了回应事件本身，我们需要订阅者知道两个东西：1.谁是发布者？ 2.发布者是否有其他事情告诉我？
  //于是在委托中我们要传入两个参数。第一个参数为发布者对象，第二个参数携带额外发送给订阅者的数据。
  public delegate void VideoEncodedEventHandler(object sender, EventArgs e);  //发布事件第一步：定义委托
  public event VideoEncodedEventHandler VideoEncoded;  //发布事件第二步：定义事件
  
  public void Encode (Video video)
  {
    Console.WriteLine("Start encoding...");
    Thread.Sleep(3000);   
    
    OnVideoEncoded();  //获取订阅者响应
  }
  
  //为了获得订阅者的回应，我们需要先定义一个方法来通知订阅者，然后通过订阅者的执行方法响应事件。这个方法即发布事件。
    //补充：任何情况下，发布者对订阅者一无所知也无需知道它们的状态。
  protected virtual void OnVideoEncoded()   //发布事件第三步：发布事件
  {
    if (VideoEncoded != null)  //验证是否有订阅者订阅此事件
      VideoEncoded(this, EventArgs.Empty);  //像调用方法一样调用事件，即获得订阅者响应。此例中发布者为当前类，暂时不附带发送数据。
  }
}

//以邮件服务为例订阅事件

class MailService
{
  //订阅事件统一格式，即订阅者“协议”方法中签名与发布者委托签名相同
  public void OnVideoEncoded (object sender, EventArgs e)
  {
    Console.WriteLine("An Email has been sent.");  //订阅者订阅事件后采取的响应
  }
}

//现在我们有了事件及其发布者和订阅者，我们需要在Client中，在这里即主函数Main()中将它们连接在一起，即签署“协议”：

static void Main(string[] args)
{
  var video = new Video();
  var videoEncoder = new VideoEncoder();  //发布者
  var mailService = new MailService();  //订阅者
  var messageService = new MessageService();  //假设有一个新的订阅者
  
  videoEncoder.VideoEncoded += mailService.OnVideoEncoded;  //将订阅者添加进事件订阅者列表，即真正实现“订阅”这一过程
  videoEncoder.VideoEncoded += messageService.OnVideoEncoded;  //随意添加新订阅者
  
  videoEncoder.Encode(video);  //执行“编码视频文件”这一过程，因为其中包含了“编码完成”事件，该事被MailService订阅，所以在结束后会获得订阅者响应
}//执行这一段程序的结果为：编码完成后邮件系统被触发并发送邮件，若有新的订阅者直接在这添加，无需更改发布者和其他订阅者的代码

//以上为事件发布 - 订阅体系中的核心内容。
//小结：定义事件基于委托，即事件类似于委托是一个容纳订阅者方法/函数的列表。直接调用则执行其包含的所有方法。通过在Client中对这个方法列表进行操作
  //如添加、删除订阅者无需更改发布者或订阅者的代码，即实现了松耦合程序结构设计。

//Q: 前面提到有办法通过EventArgs作为参数装进委托中，让发布者能发送一些额外的信息/数据给订阅者。要如何实现这个功能？
//A: 在很多情况下不同的事件需要传达不同的参数，可以通过定制事件传达参数(EventArgs)来实现。定制的参数类必须继承自EventArgs。在以上例子中，
  //我们可以定制一个VideoEventArgs类，这个类包含编码完成后的Video具体数据。如以下所示：

public class VideoEnventArgs : EventArgs
{
  public Video video { get; set; }  //我们将所有想发送出去的内容装进这个类，如定义一个video类属性来发送有关video的信息
}

//相应的发布者也需要做些修改：

class VideoEncoder
{
  public delegate void VideoEncodedEventHandler(object sender, VideoEventArgs e);  //在这里传入VideoEventArgs类参数
  public event VideoEncodedEventHandler VideoEncoded;
  
  public void Encode (Video video)
  {
    Console.WriteLine("Start encoding...");
    Thread.Sleep(3000);   
    
    OnVideoEncoded(video);  //在这里传入video参数给该方法中的VideoEncoded事件使用
  }
  
  protected virtual void OnVideoEncoded(Video video)   //实例话VideoEventArgs需要一个video实例，在这里传入该参数
  {
    if (VideoEncoded != null)
      VideoEncoded(this, new VideoEventArgs() { Video = video });  //传出一个VideoEventArgs实例，数需要传输的据都被封装在这个实例中
  }
}

//这个例子中，VideoEventArgs这个实例中封装的只有一个Video类实例，也就是在Encode()方法中传入的video实例。通过VideoEventArgs订阅者可以轻易
  //取得其中video的所有信息，如title。在订阅者中有如下操作：

class MailService
{
  public void OnVideoEncoded (object sender, VideoEventArgs e)  //相应的接收VideoEventArgs
  {
    Console.WriteLine("An Email has been sent." + e.video.title);  //像调用任何实例的内部属性一样调用VideoEventArgs的实例e
  }
}

//Q: 定义委托/事件有没有简便写法？
//A: 在.NET framwork 3.0中新加入了一套EventHandler委托类。有两种重载，分别是：

public event EventHandler name;//1. 无泛型委托：在不发送额外信息的时候使用
  
public event EventHandler<TEventArgs> name;//2. 泛型委托：在要发送额外信息给订阅者时使用

//改写后的发布者可省去自定义委托这一步骤。此例中药发送额外信息给订阅者，所以试用泛型委托。简化后的发布者如下：

class VideoEncoder
{

  public event EventHandler<VideoEventArgs> VideoEncoded;  //仅在此有变化，节约了三部走中的第一步：定义委托。使用自带事件委托顶替。
  
  public void Encode (Video video)
  {
    Console.WriteLine("Start encoding...");
    Thread.Sleep(3000);   
    
    OnVideoEncoded(video);
  }
  
  protected virtual void OnVideoEncoded(Video video)
  {
    if (VideoEncoded != null)
      VideoEncoded(this, new VideoEventArgs() { Video = video });
  }
}

//Q: 使用自带的事件委托有没有什么限制？什么时候需要自定义委托？
//A: 有限制。只有一般情况下才能用自带的事件委托简写。一般情况是指订阅者需要知道谁是发布者的情况，即至少必须有传入的第一个参入sender，如：

public delegate void VideoEncodedEventHandler(object sender);
  
//若我们不用让订阅者知道谁是发布者，即订阅者不用调用发布者属性就能做出回应，我们就不用在委托中传入sender。其实以上的例子中完全可以省略sender，如：
  
public delegate void VideoEncodedEventHandler();

//反之，在需要调用发布者属性才能做出回应的情况下，就需要sender，换句话说就可以使用自带委托简写，是否采用泛型取决于是否需要发送额外信息。
  
//Q: 如何在订阅者中调用发布者属性？
//A: 这就涉及到类中的升级(upcasting)和降级(downcasting)的概念。首先，sender定义中为object，即一切对象的基类。作为最大的类，肯定不能通过
  //sender.xxx来直接调用sender的属性。我们需要对属于object类的sender降级(downcasting)，降为它本身属于的小类，在以上例子中，sender为发布者
  //videoEncoder本身，我们要把它降级为它本来的类，即VideoEncoder类。
//类的转换可以直接通过()来转换，如：
  
var encoder = (Videoencoder)sender;

//但这是不安全方法，并不推荐强制转换。很多情况下强制转换会失败，导致抛出异常，在CS_Intermediate_LearningNote中的类的转换专题中有详细讨论。
//我们这里使用安全的降级方法，使用关键词as，如：

var encoder = sender as VideoEncoder;

//这样我们就可以直接通过encoder.xxx来直接调用VideoEncoder这个实例中的属性/方法了。

//暂时想到这么多，最后编辑日期2017/11/14
