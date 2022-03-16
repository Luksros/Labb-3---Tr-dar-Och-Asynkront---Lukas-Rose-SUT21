using Labb_3___Trådar_Och_Asynkront___Lukas_Rose_SUT21;


Cars.cars.AddRange(new List<Car>(){new() { Name = "Ford Mondeo" }, new() { Name = "Audi S7" }, new() { Name = "Volvo V70" } });
List<Task> tasks = new() { new Task(Cars.Disaster)};
foreach (Car car in Cars.cars) { tasks.Add(new Task(car.Drive)); }
foreach (Task task in tasks) { task.Start(); }
Cars.raceInit();
Cars.GetScore(); 