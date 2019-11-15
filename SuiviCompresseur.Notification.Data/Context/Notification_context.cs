using SuiviCompresseur.Notification.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Notification.Data.Context
{
    public class Notification_context : DbContext
    {
        public Notification_context(DbContextOptions<Notification_context> options) : base(options)
        {
        }


        public DbSet<EmailTo> EmailTos { get; set; }
        public DbSet<EmailFrom> EmailFroms { get; set; }


    }
}
