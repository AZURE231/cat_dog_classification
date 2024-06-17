# Cat and Dog Recognition Project

Welcome to the Cat and Dog Recognition Project! This repository contains the code for an AI-powered web API that can recognize whether an uploaded image is of a cat or a dog and identify its corresponding breed.

## Overview

This project uses ML.NET to create a machine learning model capable of classifying images of cats and dogs and determining their breeds. The model is then deployed as a web API which provides an endpoint to predict the class and breed of an uploaded image.

## Features

- **Image Classification**: Determine if the uploaded image is of a cat or a dog.
- **Breed Identification**: Identify the breed of the cat or dog in the uploaded image.
- **Web API**: Expose the model as a web API for easy integration with other applications.

## Technology Stack

- **Machine Learning**: [ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet)
- **Web API**: Built with ML.NET to serve predictions

## Dataset

The model is trained on a dataset containing images of cats and dogs from various breeds. The dataset used includes labeled images that help in identifying the species (cat or dog) and their specific breeds.
1. **Dog vs Cat**
- **Source**: [Dogs vs. Cats dataset](https://www.kaggle.com/c/dogs-vs-cats/data) available on Kaggle.
- **Description**: The dataset contains 25,000 labeled images of cats and dogs, with approximately equal numbers of images for each class.
2. **Dog breed dataset**
- **Source**: [Stanford Dogs dataset](https://www.kaggle.com/datasets/jessicali9530/stanford-dogs-dataset) available on Kaggle.
- **Description**: The dataset contains 20,580 labeled images of 120 different kind of dog breeds.
3. **Cat breed dataset**
- **Source**: [Stanford Dogs dataset](https://www.kaggle.com/datasets/jessicali9530/stanford-dogs-dataset) available on Kaggle.
- **Description**: The dataset contains 20,580 labeled images of 120 different kind of dog breeds.
## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core) (v3.1 or later)
- [ML.NET Model Builder](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet/model-builder)

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/AZURE231/cat_dog_classification.git
2. **Build the project:**
    ```bash
    dotnet build
### Running the Application
1. **Run the web API:**
    ```bash
    dotnet run --project Catvsdog_WebApi1
    ```
    This will start the web API on `http://localhost:5000`.
2. **Test the API**

    You can use tools like Postman or curl to test the API.
    ```bash
    curl -X POST http://localhost:5000/predict -F "image=@path_to_your_image.jpg"
    ```
    The API will return a JSON response indicating whether the image is of a cat or a dog and its breed.
## API endpoint
### POST /predict
* **Description:** Predicts whether the uploaded image is a cat or a dog and its breed.
* **Request:** Multipart/form-data with an image file.
* **Response:** JSON object with the prediction result.
* **Example response:**
    ```bash
    {
    "AnimalType": "Dog",
    "Breed": "Golden Retriever"
}
## Contributing
We welcome contributions to this project! If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are warmly welcome.
## License
This project is licensed under the MIT License
## Contact
For any inquiries, please contact huynhvotuan231@gmail.com.

    
