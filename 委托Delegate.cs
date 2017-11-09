//Q: 什么是委托？
//A: 委托(Delegate)是一种类，也是Object，属于引用型(Reference type)，可客户定制。

//Q: 委托是干什么用的？
//A: 委托用来调用其它类中方法，或者说它是一种指向具体函数的指针。

//Q: 为什么我们要用委托来调用其他类中的方法？直接调用不好吗？
//A: 类似于接口，使用委托调用方法最直接的好处可以让整个程序从结构上实现松耦合("Loose coupling")设计，即改变或新增某些模块几乎不用涉及到其他
//模块，又即可避免“牵一发则动全身”的尴尬情况。

//Q: 具体怎么定义/使用委托？
//A: 按照我的理解，所有“执行类”中都可以使用委托来降低耦合度。“执行类”即一个内部有多个调用其他类方法情况的类，通常表现为在内部实例化其他类作为
//参数，然后又在内部调用另外一些类的方法，并将刚才那些参数传递进去。以编辑照片这个过程为例，这个过程需要一个Photo类，用于定义什么是照片，
//照片从哪来；一个PhotoFilter类，储存具体操作照片的方法；一个PhotoProcessor类，用来执行“选中一张照片并对其进行处理”这一过程，即执行类；最后
//需要一个可执行函数，即Main()函数。
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









