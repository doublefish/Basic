类库平台：.Net Standard 2.1
Web平台：.Net Core 3.1

简介：主要通过继承和重写来实现多态，省略重复代码。

引用代码分析器：Microsoft.CodeAnalysis.CSharp，应做到没有修改提示（消息级别）

类库简介（从下往上）：

1 Adai.Core
 常用方法、扩展类库，不建议改动。

2.1 Basic.Model
实体层，实体和配置（比如状态标识枚举）。

2.2 Basic.DAL
数据库访问层，类名前缀与相关实体一一对应，只应有数据库增删改查代码，不应有业务逻辑代码。

2.3 Basic.BLL
业务逻辑层，类名前缀与相关实体一一对应，只应有业务逻辑代码，合理规划，提高代码复用率。

3 WebApi
接口层公共类库，提取类库非个性化代码，方便维护。

4.1 Basic.WebApi.Common
WebApi（公共），不应存在较复杂的业务逻辑代码，业务逻辑代码应放在业务逻辑层。

5.2 Basic.WebApi.Background
WebApi（管理后端），不应存在较复杂的业务逻辑代码，业务逻辑代码应放在业务逻辑层。

5.3 Basic.WebApi.Foreground
WebApi（用户前端），不应存在较复杂的业务逻辑代码，业务逻辑代码应放在业务逻辑层。

5.4 Basic.WebApi.Agent
WebApi（代理商），不应存在较复杂的业务逻辑代码，业务逻辑代码应放在业务逻辑层。

其它：关于全球化
业务逻辑层和接口层的资源文件读取均放在基类中。
共享资源文件在 Basic.WebApi.Languages.Shared.resx 中，通过 ResourceHelper.SharedLocalizer.GetString 访问；
业务逻辑层中使用：在Languages文件中新建同名资源文件；
控制器中使用：请声明带参数（IStringLocalizerFactory）的构造函数，在 Languages 文件中新建同名资源文件。
