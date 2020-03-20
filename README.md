# Reface.NPI.DynamicProxy

## 1 功能介绍

这是一个轻量级的 Orm 库，它的基本原理如下

1. 通过 [Castle.DynamicProxy] 生成 Dao 接口的代理类
2. 代理类拦截到的 *MethodInfo* 和 *参数* , 并通过 **[Reface.NPI]** 生成 Sql命令
3. 通过 [Dapper] 执行语句并返回对象

**目前** 仅实现了面向 SqlServer 的功能。
考虑到 [Dapper] 已有各种数据库的库，因此，将 [Reface.NPI] 接入不同的数据库理论上只需要接入不同的 [Dapper] 即可

## 2 功能定位

创建代理类，使用 AOP 的方式调用 [Reface.NPI] 的功能。
IDbConnection 和 IDbTrancation 由上层提供，本库不对数据库连接和事务进行管理。
**sql 执行**、**数据库查询结果** 与 **预期数据结构** 的映射由其它 Orm 框架执行。

## 3 使用

### 3.1 Nuget 引用

```cd
PM> Install-Package Reface.NPI.DynamicProxy -Version 1.0.1
```

### 3.2 创建 Entity

对类型加上 [Table] ，这个标签来源于 **System.ComponentModel.DataAnnotations.Schema**

### 3.3 创建 Dao 接口

该接口需要继承于 *INpiDao&lt;TEntity&gt;* ,
并根据 [Reface.NPI] 中的规范编写方法名称,

### 3.4 创建 Dao 代理类

```csharp
// IUserDao.cs
interface IUserDao
{
    User SelectById(int id); // 只需要编写方法，不需要实现
    void UpdateLoginnameById(string loginName, int id);
    bool UpdatePasswordById(string password, int id);
    void DeleteById(int id);
    bool DeleteByLoginname(string loginName);
    void Insert(User user);
    IList<User> SelectByNameLike(string name);
}
```

```csharp
// DbConnectionContext 中包含数据库连接
// 上层可以管理连接
// 代理类中不对连接管理
DbConnectionContext ctx = new DbConnectionContext(conn);
INPIImplementer imper = new NPIImplementer(ctx);
IUserDao dao = imper.Implement<IUserDao>();  // 该方法会动态生成 IUserDao 的实现类
var user = dao.SelectById(1);
```

---

**Reface.NPI.DynamicProxy.AppOfSqlite** 项目是基于 Sqlite 的一个演示项目

[Dapper]: https://github.com/StackExchange/Dapper
[Reface.NPI]: https://github.com/ShimizuShiori/Reface.NPI
[Castle.DynamicProxy]: http://www.castleproject.org/