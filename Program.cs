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
builder.Services.AddScoped<ChapterContext, ChapterContext>(); // Chama o ChapterContext , ele instancia sempre o mesmo objeto

builder.Services.AddCors(options =>
{
options.AddPolicy("CorsPolicy", builder => {
    builder.WithOrigins("http://localhost:5128")
    .AllowAnyHeader()
    .AllowAnyMethod();
});
});

//Habilitar Cors 

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

    options.DefaultAuthenticateScheme = "JwtBearer"; //Esquema de autenticação
    options.DefaultChallengeScheme = "JwtBearer"; //Esquema padrão , trafega um token no corpo da requisição
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

builder.Services.AddTransient<LivrosRepository, LivrosRepository>(); // Toda vez que é instanciando ele cria um novo objeto
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

 app.UseCors("CorsPolicy");
app.UseAuthorization();

app.Run();
