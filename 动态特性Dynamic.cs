//Q: 在C#中动态是什么？
//A: 动态(Dynamic)是C#4.0加入的新特性，让C#有时候能具有解释性语言的功能。C#本身为编译型语言，即静态语言，同类的还有Java。而解释性语言，
  //又即动态语言，属于这种类型的有Ruby, Javascript, Python等等。要理解C#中的动态特性，要先明白静态语言和动态语言的区别。这两者的区别在于，
  //静态语言中，对类型、成员、属性、方法的解释发生在编译时。比如当我们用静态语尝试访问并没有被定义的变量成员，那么编译器会告诉我们没有找到该变量。
  //动态语言中，对类型、成员、属性、方法的解释发生在运行时。所有变量在定义时都不用声明其类型，类型默认为其赋值的类型。
  //静态语言属于强类型定义语言。其安全、严谨，所有非逻辑错误都会在编译时被指出并修正；
  //动态语言属于弱类型定义语言。其最大的好处在于编码时方便快捷，不用做类型强制转换，相应的，为了保证代码安全，需要做更多的单元测试。
