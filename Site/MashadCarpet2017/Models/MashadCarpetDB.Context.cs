﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MashadCarpet.Models
{
    using MashadCarpet.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    
    public partial class MashadCarpetEntities : DbContext
    {
        public MashadCarpetEntities()
            : base("name=MashadCarpetEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AgentReq> AgentReq { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<ContactUSForm> ContactUSForm { get; set; }
        public DbSet<NewsLetters> NewsLetters { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<SIzes> SIzes { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<TicketResponse> TicketResponse { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<VisitCounter> VisitCounter { get; set; }
        public DbSet<Texts> Texts { get; set; }
        public DbSet<SliderTexts> SliderTexts { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<BlogGroups> BlogGroups { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Product_Excel> Product_Excel { get; set; }
        public DbSet<Log_LoginAttemps> Log_LoginAttemps { get; set; }
        public DbSet<ProductColorSizes> ProductColorSizes { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<Log_ProductExcells> Log_ProductExcells { get; set; }
        public DbSet<Discounts> Discounts { get; set; }
        public DbSet<Rel_Discounts_Sizes> Rel_Discounts_Sizes { get; set; }
        public DbSet<PaymentUniqeCodes> PaymentUniqeCodes { get; set; }
        public DbSet<PaymentLogs> PaymentLogs { get; set; }
    }
}
