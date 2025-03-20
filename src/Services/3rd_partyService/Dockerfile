FROM python:3.11-slim

# Set the working directory
WORKDIR /app

# Copy all files in the current directory into the container
COPY . /app

# Install dependencies listed in the requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

# Expose the port that Uvicorn will run on
EXPOSE 8081

# Run the application using Uvicorn
CMD ["uvicorn", "main:app", "--reload", "--host", "0.0.0.0", "--port", "8081"]
