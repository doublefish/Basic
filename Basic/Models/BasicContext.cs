using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Basic.Models
{
    public partial class BasicContext : DbContext
    {
        public BasicContext()
        {
        }

        public BasicContext(DbContextOptions<BasicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountBankCard> AccountBankCards { get; set; }
        public virtual DbSet<AccountCommission> AccountCommissions { get; set; }
        public virtual DbSet<AccountFund> AccountFunds { get; set; }
        public virtual DbSet<AccountMail> AccountMails { get; set; }
        public virtual DbSet<AccountPassword> AccountPasswords { get; set; }
        public virtual DbSet<AccountPromotion> AccountPromotions { get; set; }
        public virtual DbSet<AccountSm> AccountSms { get; set; }
        public virtual DbSet<AccountWithdraw> AccountWithdraws { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<CommissionRule> CommissionRules { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Dict> Dicts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Mail> Mail { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sm> Sms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPassword> UserPasswords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=mysql@123456;database=Basic;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasComment("用户");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(128)
                    .HasComment("头像地址");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18,2)")
                    .HasComment("账户余额");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasComment("电子邮箱");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(32)
                    .HasComment("名字");

                entity.Property(e => e.Freeze)
                    .HasColumnType("decimal(18,2)")
                    .HasComment("冻结金额");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(32)
                    .HasComment("证件号码");

                entity.Property(e => e.IdType).HasComment("证件类型");

                entity.Property(e => e.LastName)
                    .HasMaxLength(32)
                    .HasComment("姓氏");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(16)
                    .HasComment("手机号码");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(32)
                    .HasComment("昵称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.SecretKey)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("密钥");

                entity.Property(e => e.Sex).HasComment("性别");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Tel)
                    .HasMaxLength(16)
                    .HasComment("电话号码");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("用户名");
            });

            modelBuilder.Entity<AccountBankCard>(entity =>
            {
                entity.ToTable("AccountBankCard");

                entity.HasComment("用户银行卡");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.BankId).HasComment("银行Id");

                entity.Property(e => e.Branch)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("支行");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("卡号");

                entity.Property(e => e.Cardholder)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("持卡人");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Status).HasComment("状态");
            });

            modelBuilder.Entity<AccountCommission>(entity =>
            {
                entity.ToTable("AccountCommission");

                entity.HasComment("用户佣金");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("金额");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.OrderId).HasComment("订单Id");

                entity.Property(e => e.Rate)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("比例");

                entity.Property(e => e.Status).HasComment("状态");
            });

            modelBuilder.Entity<AccountFund>(entity =>
            {
                entity.ToTable("AccountFund");

                entity.HasComment("用户资金流水");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("金额");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("账户余额");

                entity.Property(e => e.Freeze)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("冻结金额");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("编号");

                entity.Property(e => e.Type).HasComment("类型");
            });

            modelBuilder.Entity<AccountMail>(entity =>
            {
                entity.ToTable("AccountMail");

                entity.HasComment("用户邮件");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.CheckCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasComment("验证码");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasComment("内容");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("电子邮箱");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Type).HasComment("类型");
            });

            modelBuilder.Entity<AccountPassword>(entity =>
            {
                entity.ToTable("AccountPassword");

                entity.HasComment("用户密码");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("密码");

                entity.Property(e => e.Type).HasComment("类型：1-登录密码");
            });

            modelBuilder.Entity<AccountPromotion>(entity =>
            {
                entity.ToTable("AccountPromotion");

                entity.HasComment("推广记录");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.OrderAmount)
                    .HasColumnType("decimal(18,2)")
                    .HasComment("订单金额");

                entity.Property(e => e.Orders).HasComment("订单数量");

                entity.Property(e => e.PromoterId).HasComment("推广人Id");

                entity.Property(e => e.Status).HasComment("状态");
            });

            modelBuilder.Entity<AccountSm>(entity =>
            {
                entity.HasComment("用户短信");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.CheckCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasComment("验证码");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasComment("内容");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("手机号码");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Type).HasComment("类型");
            });

            modelBuilder.Entity<AccountWithdraw>(entity =>
            {
                entity.ToTable("AccountWithdraw");

                entity.HasComment("用户提现");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("金额");

                entity.Property(e => e.BankId).HasComment("银行Id");

                entity.Property(e => e.Branch)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("支行");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("卡号");

                entity.Property(e => e.Cardholder)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("持卡人");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("编号");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.UserId).HasComment("操作用户Id");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.HasComment("文章");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Author)
                    .HasMaxLength(64)
                    .HasComment("作者");

                entity.Property(e => e.Clicks).HasComment("点击数");

                entity.Property(e => e.Content).HasComment("内容");

                entity.Property(e => e.Cover)
                    .HasMaxLength(128)
                    .HasComment("封面地址");

                entity.Property(e => e.Favorites).HasComment("收藏数");

                entity.Property(e => e.IsStick)
                    .HasColumnType("bit(1)")
                    .HasComment("是否置顶");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Sections)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("版块");

                entity.Property(e => e.Shares).HasComment("分享数");

                entity.Property(e => e.Source)
                    .HasMaxLength(64)
                    .HasComment("来源");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Summary)
                    .HasMaxLength(512)
                    .HasComment("摘要");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasComment("标题");

                entity.Property(e => e.UserId).HasComment("用户Id");
            });

            modelBuilder.Entity<CommissionRule>(entity =>
            {
                entity.ToTable("CommissionRule");

                entity.HasComment("佣金规则");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("佣金金额");

                entity.Property(e => e.Month).HasComment("月份");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.ProductId).HasComment("产品Id");

                entity.Property(e => e.Rate)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("佣金比例");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Year).HasComment("年份");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasComment("客户");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasComment("生日");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("编码");

                entity.Property(e => e.Education).HasComment("教育程度");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasComment("邮箱地址");

                entity.Property(e => e.Faith).HasComment("宗教信仰");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("名字");

                entity.Property(e => e.HealthStatus)
                    .HasMaxLength(256)
                    .HasComment("健康状况");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(32)
                    .HasComment("证件号码");

                entity.Property(e => e.IdType).HasComment("证件类型");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("姓氏");

                entity.Property(e => e.MaritalStatus).HasComment("婚姻状况");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(16)
                    .HasComment("手机号码");

                entity.Property(e => e.Nation).HasComment("民族");

                entity.Property(e => e.NativePlace).HasComment("籍贯");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.PoliticalStatus).HasComment("政治面貌");

                entity.Property(e => e.Sex).HasComment("性别");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Tel)
                    .HasMaxLength(16)
                    .HasComment("电话号码");
            });

            modelBuilder.Entity<Dict>(entity =>
            {
                entity.ToTable("Dict");

                entity.HasComment("配置");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("编码");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("名称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.ParentId).HasComment("父节点Id");

                entity.Property(e => e.Parents)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("父节点Ids");

                entity.Property(e => e.Sequence).HasComment("序号");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Value).HasComment("值");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasComment("员工");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(64)
                    .HasComment("地址");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasComment("生日");

                entity.Property(e => e.Certificates)
                    .HasMaxLength(64)
                    .HasComment("证书");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("编码");

                entity.Property(e => e.ComputerSkills)
                    .HasMaxLength(64)
                    .HasComment("计算机技能");

                entity.Property(e => e.Education).HasComment("教育程度");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasComment("电子邮箱");

                entity.Property(e => e.Faith).HasComment("宗教信仰");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("名字");

                entity.Property(e => e.HealthStatus)
                    .HasMaxLength(256)
                    .HasComment("健康状况");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(32)
                    .HasComment("证件号码");

                entity.Property(e => e.IdType).HasComment("证件类型");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(64)
                    .HasComment("职称");

                entity.Property(e => e.LanguageSkills)
                    .HasMaxLength(64)
                    .HasComment("语言能力");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("姓氏");

                entity.Property(e => e.Major)
                    .HasMaxLength(64)
                    .HasComment("专业");

                entity.Property(e => e.MaritalStatus).HasComment("婚姻状况");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(16)
                    .HasComment("手机号码");

                entity.Property(e => e.Nation).HasComment("民族");

                entity.Property(e => e.NativePlace).HasComment("籍贯");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Photo)
                    .HasMaxLength(128)
                    .HasComment("照片");

                entity.Property(e => e.PoliticalStatus).HasComment("政治面貌");

                entity.Property(e => e.Post).HasComment("岗位");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(16)
                    .HasComment("邮政编码");

                entity.Property(e => e.School)
                    .HasMaxLength(64)
                    .HasComment("毕业院校");

                entity.Property(e => e.Sex).HasComment("性别");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Tel)
                    .HasMaxLength(16)
                    .HasComment("电话号码");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.HasComment("意见反馈");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Content)
                    .HasMaxLength(2048)
                    .HasComment("内容");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasComment("电子邮箱");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(16)
                    .HasComment("手机号码");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Score)
                    .HasColumnType("float(2,1)")
                    .HasComment("评分");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.WeChat)
                    .HasMaxLength(32)
                    .HasComment("微信账号");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("File");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("编码");

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasComment("后缀名");

                entity.Property(e => e.Length).HasComment("长度");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("名称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("路径");

                entity.Property(e => e.Status).HasComment("状态");
            });

            modelBuilder.Entity<Mail>(entity =>
            {
                entity.HasComment("邮件");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.CheckCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasComment("验证码");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasComment("内容");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("电子邮箱");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Type).HasComment("类型");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.HasComment("功能菜单");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.ApiUrl)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("接口地址");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("编码");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("图标");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("名称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.PageUrl)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("页面地址");

                entity.Property(e => e.ParentId).HasComment("父节点Id");

                entity.Property(e => e.Parents)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("父节点Ids");

                entity.Property(e => e.Sequence).HasComment("序号");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Type).HasComment("类型：1-系统，2-目录，3-页面，4-功能");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasComment("订单");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AccountId).HasComment("用户Id");

                entity.Property(e => e.AdultPrice)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("成人价格");

                entity.Property(e => e.Adults).HasComment("成年人数");

                entity.Property(e => e.ChildPrice)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("儿童价格");

                entity.Property(e => e.Children).HasComment("儿童人数");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasComment("出发日期");

                entity.Property(e => e.DiscountId).HasComment("折扣Id");

                entity.Property(e => e.DiscountInfo)
                    .HasMaxLength(256)
                    .HasComment("折扣信息");

                entity.Property(e => e.DiscountPrice)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("折扣价格");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasComment("手机号码");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("编号");

                entity.Property(e => e.OriginalPrice)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("原始价格");

                entity.Property(e => e.ProductId).HasComment("产品Id");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("成交总价");

                entity.Property(e => e.UserId).HasComment("操作用户Id");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasComment("产品");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.Book).HasComment("预定说明");

                entity.Property(e => e.Clicks).HasComment("点击次数");

                entity.Property(e => e.Cost).HasComment("费用说明");

                entity.Property(e => e.Cover)
                    .HasMaxLength(256)
                    .HasComment("封面地址");

                entity.Property(e => e.Cover1)
                    .HasMaxLength(256)
                    .HasComment("封面地址");

                entity.Property(e => e.Feature).HasComment("特色");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasComment("名称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Notice).HasComment("预定须知");

                entity.Property(e => e.Orders).HasComment("订单数");

                entity.Property(e => e.Overview)
                    .HasMaxLength(1024)
                    .HasComment("概要");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("价格");

                entity.Property(e => e.Recommends).HasComment("推荐人数");

                entity.Property(e => e.Sequence).HasComment("序号");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Tags)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("标签");

                entity.Property(e => e.Themes)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("主题");

                entity.Property(e => e.Type).HasComment("类型");
            });

            modelBuilder.Entity<ProductDiscount>(entity =>
            {
                entity.ToTable("ProductDiscount");

                entity.HasComment("产品折扣");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("金额");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("名称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("描述");

                entity.Property(e => e.ProductId).HasComment("产品Id");

                entity.Property(e => e.Rate)
                    .HasColumnType("decimal(18,4)")
                    .HasComment("比例");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Total).HasComment("总数");

                entity.Property(e => e.Used).HasComment("已用数量");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage");

                entity.HasComment("产品图片");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasComment("图片地址");

                entity.Property(e => e.ProductId).HasComment("产品Id");

                entity.Property(e => e.Sequence).HasComment("序号");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.HasComment("地域");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.AreaCode)
                    .HasMaxLength(64)
                    .HasComment("地区代码");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("编码");

                entity.Property(e => e.EnName)
                    .HasMaxLength(64)
                    .HasComment("英文名称");

                entity.Property(e => e.FullName)
                    .HasMaxLength(64)
                    .HasComment("全称");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("名称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.ParentId).HasComment("父节点Id");

                entity.Property(e => e.Parents)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("父节点");

                entity.Property(e => e.Pinyin)
                    .HasMaxLength(64)
                    .HasComment("拼音");

                entity.Property(e => e.Sequence).HasComment("序号");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(64)
                    .HasComment("邮政编码");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasComment("角色");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("编码");

                entity.Property(e => e.Menus)
                    .HasColumnType("varchar(4096)")
                    .HasComment("菜单");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("名称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Status).HasComment("状态");
            });

            modelBuilder.Entity<Sm>(entity =>
            {
                entity.HasComment("短信记录");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.CheckCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasComment("验证码");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(512)
                    .HasComment("内容");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("手机号码");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Type).HasComment("类型");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasComment("用户");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(128)
                    .HasComment("头像地址");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .HasComment("电子邮箱");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(32)
                    .HasComment("名字");

                entity.Property(e => e.LastName)
                    .HasMaxLength(32)
                    .HasComment("姓氏");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(16)
                    .HasComment("手机号码");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(32)
                    .HasComment("昵称");

                entity.Property(e => e.Note)
                    .HasMaxLength(512)
                    .HasComment("说明");

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("角色");

                entity.Property(e => e.SecretKey)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("密钥");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Tel)
                    .HasMaxLength(16)
                    .HasComment("电话号码");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("用户名");
            });

            modelBuilder.Entity<UserPassword>(entity =>
            {
                entity.ToTable("UserPassword");

                entity.HasComment("用户密码");

                entity.Property(e => e.Id).HasComment("ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("密码");

                entity.Property(e => e.Type).HasComment("类型：1-登录密码");

                entity.Property(e => e.UserId).HasComment("用户Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
