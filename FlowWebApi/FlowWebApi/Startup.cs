using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowWebApi.Dto;
using FlowWebApi.Dtos;
using FlowWebApi.Entities;
using FlowWebApi.Repositories;
using FlowWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace FlowWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

#if DEBUG

            services.AddTransient<IMailService,LocalMailService>();
#else
             services.AddTransient<IMailService,CloudMailService>();
#endif
            var connectionString = Configuration["connectionStrings:productionInfoDbConnectionString"];
            services.AddDbContext<MyContext>(o=>o.UseSqlServer(connectionString));

            services.AddScoped<IFlowRepository, FlowRepository>();
            //json返回结果去掉camel case
            //services.AddMvc().AddJsonOptions(op=> {
            //    if (op.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
            //    {
            //        resolver.NamingStrategy = null;
            //    }
            //});

            //services.AddMvc().AddMvcOptions(op=> {
            //    op.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory,MyContext context)
        {
            loggerFactory.AddNLog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            context.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(a=> {
                a.CreateMap<Entities.Flow, ProductWithoutMaterialDto>();
                a.CreateMap<Entities.Flow, Dto.Flow>();
                a.CreateMap<FlowCreation, Entities.Flow>();
                a.CreateMap<DtoPut.FlowModification,Entities.Flow>();
            });

            app.UseMvc();
        }
    }
}
