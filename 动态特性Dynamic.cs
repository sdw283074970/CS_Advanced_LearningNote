//Q: 在C#中动态是什么？
//A: 动态(Dynamic)是C#4.0加入的新特性，让C#有时候能具有解释性语言的功能。C#本身为编译型语言，即静态语言，同类的还有Java。而解释性语言，
  //又即动态语言，属于这种类型的有Ruby, Javascript, Python等等。要理解C#中的动态特性，要先明白静态语言和动态语言的区别。这两者的区别在于，
  //静态语言中，对类型、成员、属性、方法的解释发生在编译时。比如当我们用静态语尝试访问并没有被定义的变量成员，那么编译器会告诉我们没有找到该变量。
  //动态语言中，对类型、成员、属性、方法的解释发生在运行时。所有变量在定义时都不用声明其类型，类型默认为其赋值的类型。
  //静态语言属于强类型定义语言。其安全、严谨，所有非逻辑错误都会在编译时被指出并修正；
  //动态语言属于弱类型定义语言。其最大的好处在于编码时方便快捷，不用做类型强制转换，相应的，为了保证代码安全，需要做更多的单元测试。

//Q: 为什么要在C#中增加动态特性？
//A: 为了增强C#与COM(即组件对象模型Component Object Model,如写offic类型的应用)和动态语言(如IronPython)的交互能力。如果没有动态特性，那么就需要用
  //到反射(Reflection)，这是C#中的一个复杂且略混乱的特性，其为一种通过检视类的元数据(Metadata)来获得信息，由此来访问这个类的属性和方法的一种方法。
  //举一个不用动态特性而用反射的例子：

Static void Main(string[] args)
{
  object obj = "Hellow World";
  
  //obj为object的实例，当我们要访问其中的方法的时候，可以直接访问
  var hash = obj.GetHashCode();
  
  //如果使用反射，那么就得这样写
  var methodInfo = obj.GetType().GetMethod("GetHashCode");
  methodInfo.Invoke(null, null);
}

//以上仅为一个反射例子，这里不作反射的多讨论，未来会专开一篇详解。回到问题，为什么要使用动态特性，避免使用反射？其最核心的原因是反射会降低性能，
  //同时在编译的时候无法保证类型的安全性。

//Q: 动态特性有什么用？如何使用？
//A: 当我们给一个变量定义为动态(dynamic)后，该变量的类型就是动态型，其具体类型与赋予其值的类型相同，每进行一次赋值其类型都可以被改变而不用强制转换。
  //这点很符合动态语言的特性。要使用C#动态特性，使用关键词dynamic. 举个具体的例子：

static void Main(string[] args)
{
  dynamic str = "Hello World";  //在这里str在编译时的类型为dynamic，运行时的类型string
  str = 10;  //这里str在编译时的类型仍然为dynamic，但运行时的类型变为为int
  
  dynamic a = 10;  //编译时为dynamic，运行时为int
  dynamic b = 5;  //编译时为dynamic，运行时为int
  var c = a + b;  //编译时为dynamic，运行时为int
  
  int i = 5;
  dynamic d = i;  //d在编译时为dynamic，运行时为int
  long l = d;  //因为d在运行时为int，int转换为long时不需要进行升降级操作，同理d也不需要
}
