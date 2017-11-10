//Q: 什么是委托？
//A: 委托(Delegate)是一种类，也是Object，属于引用型(Reference type)，可客户定制。

//Q: 委托是干什么用的？
//A: 委托用来调用其它类中方法，或者说它是一种指向具体函数的指针。

//Q: 为什么我们要用委托来调用其他类中的方法？直接调用不好吗？
//A: 类似于接口(Interface)，使用委托调用方法最直接的好处可以让整个程序从结构上实现松耦合("Loose coupling")设计，即改变或新增某些模块几乎不用
//涉及到其他模块，又即可避免“牵一发则动全身”的尴尬情况。“委托”这个命名就能很好说明了它的本质，即自己不做操作，只是告诉代理去哪做，以及做什么，但
//具体怎么做由代理操心。如，在餐厅中，厨师告诉采购员需要什么食材，在哪里可以搞到这些食材，以及到达目的地的所有可行方法，但实际上采用哪些方法去搞到
//食材，厨师并不关心。在有委托的结构中，Client就是采购员，决定调用哪些方法。

//Q: 具体怎么定义/使用委托？
//A: 按照我的理解，所有“执行类”中都可以使用委托来降低耦合度。“执行类”即一个内部有多个调用其他类方法情况的类，通常表现为在内部实例化其他类作为
//参数，然后又在内部调用另外一些类的方法，并将刚才那些参数传递进去。以编辑照片的过程为例，这个过程需要:
  //1. 一个Photo类，用于定义什么是照片，照片从哪来；
  //2. 一个PhotoFilter类，储存具体操作照片的方法；
  //3. 一个PhotoProcessor类，用来执行“选中一张照片并对其进行处理”这一过程，即执行类；
  //4. 一个可执行函数，即Main()函数。
//先看一个简单的Photo类的模拟

class Photo
{
  public static Photo load(string path) //load方法,读取一个路径的照片
  {
    //...文件系统具体操作
    return new Photo(); //返回一个Photo实例
  }
  public void save()  //储存方法
  {
    //...文件系统具体操作
  }
}// 这个类基本不会涉及到修改

//然后是PhtoFilter类

class PhotoFilters
{
  public void ApplyBrightness(Photo photo)  //应用增加亮度的方法
  {
    Console.WriteLine("Apply Brightness");
  }
  public void ApplyContrast(Photo photo)  //应用增加对比度的方法
  {
    Console.WriteLine("Apply Brightness");
  }
  public void Resize(Photo photo)  //应用调整大小的方法
  {
    Console.WriteLine("Resize photo");
  }
}//在实际情况中，这个类可能会做最多变动，如方法名称、传入参数类型改变，但我们不希望其他类也要随着改变而需要改代码

//然后是使用委托之前的PhotoProcessor类

class PhotoProcessor
{
  public void Process(string path)  //这是执行“编辑照片”的方法
  {
    var photo = Photo.load(path);  //通过调用Photo类的load路径方法实例photo对象

    var filters = new PhotoFilter();  //实例化滤镜
    filters.ApplyBrightness(photo);  //调用PhotoFilter类的改变亮度方法
    filters.ApplyContrast(photo);  //调用PhotoFilter类的改变对比度方法
    filters.Resize(photo);  //调用PhotoFilter类的调整大小方法
    photo.save();  //储存
  }
}//这个类与PhotoFilter类处于高度耦合状态。即一旦PhotoFilter中的某些方法命名、参数构造发生了改变，这个类的代码也需要重写。因为懒，我们不想这样。

//以下是主函数
static void Main(string[] args)
{
  var processor = new PhotoProcessor();
  processor.Process("c:\photo.jpg");
}

//以上四个部分构成了“编辑照片”这一过程。中间提到了PhotoProcessor类与PhotoFilter类的高耦合度情况，我们可以使用委托来改善这一情况。
//委托的关键词delegate，维持Photo类不变(也没什么好变的)，在“执行类”中新建委托

class PhotoProcessor
{
  //新建委托。委托命名规则为指向类的类名+Handler，传递的参数和类型须与委托执行的方法构造器保持一致
  public delegate void PhotoFilterHandler(Photo photo);  
  
  public void Process(string path, PhotoFilterHandler filterHandler)  //这是执行“编辑照片”的方法, 此方法新增委托类参数
  {
    var photo = Photo.load(path);  //通过调用Photo类的load路径方法实例photo对象
    
    //指向在PhotoFilter中的所有要且只要传递photo参数的方法，相当于一个格式，满足这个格式的方法才能被委托执行，具体调用哪些这里并不关心
    filterHandler(photo);  
    //var filters = new PhotoFilter();  无需实例化滤镜
    //filters.ApplyBrightness(photo);这三条不再有用
    //filters.ApplyContrast(photo);
    //filters.Resize(photo);
    photo.save();  //储存
  }//在使用了委托的情况下，无论PhotoFilter怎么改，PhotoProcessor都不用跟着改，相应的在Main()中改代码就好了
  
//以下是应用委托后的Main()方法
static void Main(string[] args)
{
  var processor = new PhotoProcessor();
  var filters = new PhotoFilters();
  
  //实例化委托，并初始指定这个委托指向的方法，这个方法的构造器必须与委托定义中的构造器结构相同，即满足格式，都是传入一个Photo类对象的方法
  PhotoProcessor.PhotoFilterHandler filterHandler = filters.ApplyBrightness;  
  filterHandler += filter.ApplyContrast;  //委托是一个方法列表，用+=添加其他的方法，一旦这个委托执行，列表中的所有方法都将执行
  
  //...这里可以随意增加或删减被委托执行的方法，不用在PhotoProcessor中改，即实现了松耦合设计
  //...补充，任何符合格式的方法都可以加入到这个委托中，不一定是得在PhotoFilter中的方法，这样整个结构有如80岁老人牙齿一样松动
  
  processor.Process("C:\photo.jpg", filterHandler);  //将以上被实例化的委托传入Processor执行函数即可执行委托中的方法
}//如果PhotoFilter涉及到改动，则只需要改这里的代码就可以了

//Q: 写太多太复杂了，有没有简单点使用委托的办法？
//A: 在.NET框架中有两个自带的委托定义，可直接调用省去新建委托的步骤，它们分别是Action和Func，接下来分别阐述
  //1.Action有两种，非泛型和泛型，其中泛型拥有16种重载，即可以传递1~16个类型参数。指向的方法必须为void，即无返回值
  //2.Func只有泛型的情况，也可以传递1~16个类型参数，但是指向的方法必须有返回值，返回值类型在<>中最后一个定义，如:
//Func<int i, string s>表示指向的方法传入参数类型为一个int类，返回一个string类的值

//Q: 那么Action和Func要怎么用？
//A: 接着以以上的例子为例，继续改造“编辑照片”这个过程。在“执行类”PhotoProcessor中:
  
class PhotoProcessor
{
  //public delegate void PhotoFilterHandler(Photo photo);  删掉自己定义的委托
  
  //因为所有Filter中的方法都是void，所以这里我们用Action<>不用Func<>
  public void Process(string path, Action<Photo> filterHandler)  //<>中就是定义的格式，所有方法必须有<>中的类作为参数才能被正确指向
  {
    //其它部分保持不变
    var photo = Photo.load(path);  
    filterHandler(photo);  
    photo.save();  
  }
  
//这样简洁了很多，同样Main()中对委托实例化的部分也要改，如
  
static void Main(string[] args)
{
  var processor = new PhotoProcessor();
  var filters = new PhotoFilters();
  
  Action<Photo> filterHandler = filters.ApplyBrightness;  //直接这样就能实例化委托，其他不用改变
    
  filterHandler += filter.ApplyContrast;
  processor.Process("C:\photo.jpg", filterHandler);
}
//如果指向方法有返回值，则直接把Action<>替换成Func<>就好了，记得Func<>泛型中最后一个一定是返回值类型
  
//Q: 开始说委托和接口(Interface)类似，都是为了设计出松耦合的程序结构，那什么时候用委托，什么时候用接口？
//A: 在都可以用的情况下全屏个人喜好。但是在以下情况得用委托:
  //1.涉及到事件，在委托与事件章节会详细讨论；
  //2.不需要调用到被执行对象中的其他属性和其他方法。如在编辑照片例子中，我们不需要调用PhotoFilter的数量属性，也不需要它的“氪金解锁新滤镜”方法。

//暂时想到这么多，最后更新2017年11月10日




