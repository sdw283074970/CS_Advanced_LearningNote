//Q: 什么是事件？事件有什么用？
//A: 事件(Event)是对象之间沟通交流的机制，被用来构造松耦合程序结构和拓展功能。
  //如一个对象VideoEncoder在编码完成后另一个对象MailService就会知道并发送编码完成的Email，这个过程就需要用到事件来作为触发机关。

//Q: 事件是怎么工作的？原理是什么？
//A: 构建一个事件需要用到发布(Publish) - 订阅(Subscribe)体系。即一个对象发布事件，其他任何对这个事件感兴趣的对象订阅这个事件并做出相应反应。
  //在以上例子中，VideoEncoder就是事件发布者，MailService就是订阅者之一，“编码完成”就是事件，“编码完成发送邮件”就是事件出发后订阅者做出的反应。
  //如何形成这个体系？这里就需要一个类似协议的东西，只要发布者订阅者双方签署了这个协议，那么订阅者就会对事件的触发做出反应。

//Q: 那么这个“协议”如何签署？
//A: 这个“协议”就是委托，通过委托感知订阅者的eventHandler方法中的签名。当事件发布者触发事件，为了通知订阅者，我们需要一个返回类型为void的方法，
  //方法签名必须与发布者的委托签名相同，即相同的参数传递进去，即发布者委托签名与订阅者void方法签名相同，则完成签署这个“协议”。
  
//Q: 发布者如何发布事件？订阅者又如何订阅事件？
//A: 发布者发布事件只需跟随三步走。
  //1. 定义一个委托；
  //2. 定义一个基于委托的事件；
  //3. 发布事件。
  //订阅者订阅事件则只用定义一个方法签名与发布者委托相同的void方法即可。以视频编码器为例，使用事件之前的状态为：
  
class Video
{
  public string{ get; set;}
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
  //因为事件为编码完成后的事件，所以前半部分命名为VideoEncoded，后部分为统一标准EventHandler表明这是事件发布委托
  //第一个参数为发布者对象，第二个参数携带额外发送给订阅者的数据
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
  public void OnVideoEncoded (object sender, VideoEventArgs e)
  {
    Console.WriteLine("An Email has been sent." + e.video.title);  //订阅者订阅事件后采取的响应
  }
}

//现在我们有了事件及其发布者和订阅者，我们需要在Client中，在这里即主函数Main()中将它们连接在一起

static void Main(string[] args)
{
  var video = new Video();
  var videoEncoder = new VideoEncoder();  //发布者
  var mailService = new MailService();    //订阅者
  
  videoEncoder.VideoEncoded += mailService.OnVideoEncoded;  //将订阅者添加进事件订阅者列表
  
  videoEncoder.Encode(video);  /执行“编码视频文件”这一过程，因为其中包含了“编码完成”事件，该事被MailService订阅，所以在结束后会获得订阅者响应
}//执行这一段程序的结果为：编码完成后邮件系统被触发并发送邮件

//以上为事件发布 - 订阅体系中的核心内容。



