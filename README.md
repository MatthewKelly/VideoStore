# VideoStore

Coding exam for creating a Video Store with WebAPI

#Brief

You are a developer for a company that sells movie DVDs.
The company website uses a third party datasource to manage its movies. The third party datasource is provided in MoviesLibrary.dll.

The following methods are provided in the third party datasource “MovieDataSource”
-	GetAllData
-	GetDataById
-	Create
-	Update

You will need to write a service (using C#.Net) which will be used by the company website to consume the third party datasource.
This service should be able to perform the following:
1) Fetch movies from the third party datasource.
2) Return movies in a sorted order by any of the movie attributes. (Except for the field “Cast”)
3) Search across all fields within the movie.
4) Insert new movie
5) Update existing movie.

Note:  Calling the third party data source is costly and data only gets updated every 24 hours. 
