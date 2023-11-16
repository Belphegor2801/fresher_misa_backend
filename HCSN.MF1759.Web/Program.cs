using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using HCSN.MF1759.Infrastructure;
using HCSN.MF1759.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values.SelectMany(x => x.Errors);

        return new BadRequestObjectResult(new EndPointException()
        {
            ErrorCode = 400,
            UserMessage = ResourceVN.Error_Input,
            DevMessage = ResourceVN.Error_Input,
            TraceId = "",
            MoreInfor = "",
            Errors = errors,
        }.ToJson() ?? "");
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conectionString = builder.Configuration["ConnectionString"];

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(conectionString));


//Department
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

//Fixed asset
builder.Services.AddScoped<IFixedAssetManager, FixedAssetManager>();
builder.Services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
builder.Services.AddScoped<IFixedAssetService, FixedAssetService>();
builder.Services.AddScoped<IFixedAssetExcelHandler, FixedAssetExcelHandler>();

//Fixed asset category
builder.Services.AddScoped<IFixedAssetCategoryManager, FixedAssetCategoryManager>();
builder.Services.AddScoped<IFixedAssetCategoryRepository, FixedAssetCategoryRepository>();
builder.Services.AddScoped<IFixedAssetCategoryService, FixedAssetCategoryService>();

//TransferDocument
builder.Services.AddScoped<ITransferDocumentManager, TransferDocumentManager>();
builder.Services.AddScoped<ITransferDocumentRepository, TransferDocumentRepository>();
builder.Services.AddScoped<ITransferDocumentService, TransferDocumentService>();

//TransferDocumentDetails
builder.Services.AddScoped<ITransferDocumentDetailsManager, TransferDocumentDetailsManager>();
builder.Services.AddScoped<ITransferDocumentDetailsRepository, TransferDocumentDetailsRepository>();
builder.Services.AddScoped<ITransferDocumentDetailsService, TransferDocumentDetailsService>();

//Recipient
builder.Services.AddScoped<IRecipientRepository, RecipientRepository>();
builder.Services.AddScoped<IRecipientService, RecipientService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
