using Cat_breed_classification;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// Register the PredictionEnginePools
builder.Services.AddPredictionEnginePool<Catvsdog.ModelInput, Catvsdog.ModelOutput>()
    .FromFile("catvsdog.mlnet");
builder.Services.AddPredictionEnginePool<dog_breed.ModelInput, dog_breed.ModelOutput>()
    .FromFile("dog_breed.mlnet");
builder.Services.AddPredictionEnginePool<cat_breed.ModelInput, cat_breed.ModelOutput>()
    .FromFile("cat_breed.mlnet");

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:3000;   ")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Register services for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Description = "Docs for my API", Version = "v1" });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root (http://localhost:<port>/)
});

// Define prediction route & handler
app.MapPost("/predict", async (HttpRequest request,
    PredictionEnginePool<Catvsdog.ModelInput, Catvsdog.ModelOutput> catVsDogPredictionEnginePool,
    PredictionEnginePool<dog_breed.ModelInput, dog_breed.ModelOutput> dogBreedPredictionEnginePool,
    PredictionEnginePool<cat_breed.ModelInput, cat_breed.ModelOutput> catBreedPredictionEnginePool) =>
{
    if (!request.HasFormContentType || !request.Form.Files.Any())
    {
        return Results.BadRequest("No file uploaded.");
    }

    var file = request.Form.Files[0];

    using var memoryStream = new MemoryStream();
    await file.CopyToAsync(memoryStream);

    var imageBytes = memoryStream.ToArray();
    var catVsDogInput = new Catvsdog.ModelInput
    {
        ImageSource = imageBytes
    };

    // Predict if it's a cat or a dog
    var catVsDogPrediction = catVsDogPredictionEnginePool.Predict(catVsDogInput);

    // Initialize the response structure
    var response = new
    {
        AnimalType = "",
        Breed = ""
    };

    if (catVsDogPrediction.PredictedLabel == "Dog")
    {
        var dogBreedInput = new dog_breed.ModelInput
        {
            ImageSource = imageBytes
        };

        var dogBreedPrediction = dogBreedPredictionEnginePool.Predict(dogBreedInput);
        var dogBreedName = FormatBreedName(dogBreedPrediction.PredictedLabel.Split('-').Last());


        response = new
        {
            AnimalType = "Dog",
            Breed = dogBreedName
        };
    }
    else if (catVsDogPrediction.PredictedLabel == "Cat")
    {
        var catBreedInput = new cat_breed.ModelInput
        {
            ImageSource = imageBytes
        };

        var catBreedPrediction = catBreedPredictionEnginePool.Predict(catBreedInput);

        response = new
        {
            AnimalType = "Cat",
            Breed = catBreedPrediction.PredictedLabel
        };
    }
    else
    {
        return Results.BadRequest("Unable to classify the image.");
    }

    return Results.Ok(response);
});

string FormatBreedName(string breed)
{
    // Replace underscores with spaces and capitalize each word
    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(breed.Replace('_', ' '));
}

// Run app
app.Run();
