
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Monitoring;
using Monitoring.Queue.Event;
using RabbitMQ.Client.Core.DependencyInjection;
using System;

namespace Challenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // Rabbit
            var rabbitMqSection = Configuration.GetSection("RabbitMq");
            var exchangeSection = Configuration.GetSection("RabbitMqExchange");

            services.AddRabbitMqClient(rabbitMqSection)
                .AddExchange("amq.direct", exchangeSection)
                .AddAsyncMessageHandlerSingleton<GeneralAsyncMessageHandler>("general");

            var serviceProvider = services.BuildServiceProvider();
            var queueService = serviceProvider.GetRequiredService<IQueueService>();
            queueService.StartConsuming();


            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();

            // Modulos
            builder.RegisterModule(new MonitoringModule());

            builder.Populate(services);
            var container = builder.Build();
            // Create the IServiceProvider based on the container.

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
