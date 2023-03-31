using Chapter.WebApi.Contexts; 
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<ChapterContext, ChapterContext>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //Configuração do Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ChapterApi",
                Version = "v1"
            });
        });
        //forma de autenticação 
        builder.Services.AddAuthentication(options =>
        {

            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        })
        //Parametros de validação do token 
        .AddJwtBearer("JwtBearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                //Valida queme stá solicitando 
                ValidateIssuer = true,
                //Valida quem está recebendo
                ValidateAudience = true,
                //Define se o tempo de inspiração será valido
                ValidateLifetime = true,
                //Criptografia e validação da Chave de autenticação
                IssuerSigningKey = new
                SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapterapi-chave-autenticacao")),
                //Valida o tempo de inspiração do token
                ClockSkew = TimeSpan.FromMinutes(30),
                //Nome da issur, Origem 
                ValidIssuer = "chapterapi.webapi",
                //Nome do audience, destinatario
                ValidAudience = "chapterapi.webapi"
            };
        });

        builder.Services.AddTransient<LivrosRepository, LivrosRepository>();
        builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();

        //Ativando o middleware para o swagger
        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c => 
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChapterApi v1"));

        app.UseRouting();

        app.UseAuthentication();//Habilitar autenticação
        app.UseAuthorization();//Habilitar a autorização 


        app.UseEndpoints(endpoints =>
         { 
            endpoints.MapControllers();
         });

        app.UseAuthorization();

        app.Run();
    