using MySql.Data.MySqlClient;
namespace HelloWorld
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Project> Movies { get; set; }
    }

    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectMaker { get; set; }
        public List<Person> Actors { get; set; }
    }

    public class ProjectActor
    {
        public int ProjectId { get; set; }
        public int PersonId { get; set; }
    }

    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<Project> Movies { get; set; }
    }

    public class GroupProject
    {
        public int GroupId { get; set; }
        public int ProjectId { get; set; }
    }

    public class Program
    {
        public static string ConnectionString = @"server=localhost;userid=root;password=LMTpass2019*;database=sakila;";

        public static MySqlConnection OpenConnection()
        {

            MySqlConnection conn = new MySqlConnection(ConnectionString);
            conn.Open();

            return conn;
        }

        public static void SelectFromTable(string table)
        {
            MySqlConnection localConn = OpenConnection();
            var statement = "Select * from " + table + ";";
            var command = new MySqlCommand(statement, localConn);
            using MySqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                        rdr.GetString(2));
            }
        }

        public static List<Person> SelectFromTableInModelPerson(string table)
        {
            MySqlConnection localConn = OpenConnection();
            var statement = "Select * from " + table + ";";
            var command = new MySqlCommand(statement, localConn);
            using MySqlDataReader rdr = command.ExecuteReader();
                List<Person> list = new List<Person>();
                while (rdr.Read())
                {
                    Person person = new Person();
                    person.PersonId = rdr.GetInt32(0);
                    person.Name = rdr.GetString(1) + " " + rdr.GetString(2);
                    person.FirstName = rdr.GetString(1);
                    person.LastName = rdr.GetString(2);
                    list.Add(person);
                }
                return list;
            }

          

        public static List<Project> SelectFromTableInModelProject(string table)
        {
            MySqlConnection localConn = OpenConnection();
            var statement = "Select * from " + table + ";";
            var command = new MySqlCommand(statement, localConn);
            using MySqlDataReader rdr = command.ExecuteReader();


                List<Project> list = new List<Project>();
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = rdr.GetInt32(0);
                    project.ProjectName = rdr.GetString(1);
                    project.ProjectDescription = rdr.GetString(2);
                    list.Add(project);
                }
            return list;    
        }

        public static List<Group> SelectFromTableInModelGroup(string table)
        {
            MySqlConnection localConn = OpenConnection();
            var statement = "Select * from " + table + ";";
            var command = new MySqlCommand(statement, localConn);
            using MySqlDataReader rdr = command.ExecuteReader();


            List<Group> list = new List<Group>();
            while (rdr.Read())
            {
                Group group = new Group();
                group.GroupId = rdr.GetInt32(0);
                group.GroupName = rdr.GetString(1);
                list.Add(group);
            }
            return list;
        }

        public static List<ProjectActor> SelectFromTableInModelProjectPerson(string table)
        {
            MySqlConnection localConn = OpenConnection();
            var statement = "Select * from " + table + ";";
            var command = new MySqlCommand(statement, localConn);
            using MySqlDataReader rdr = command.ExecuteReader();
         
                List<ProjectActor> list = new List<ProjectActor>();
                while (rdr.Read())
                {
                    ProjectActor projectactor = new ProjectActor();
                    projectactor.ProjectId = rdr.GetInt32(0);
                    projectactor.PersonId = rdr.GetInt32(1);
                    list.Add(projectactor);
                }
            return list;
            }

        public static List<GroupProject> SelectFromTableInModelGruopProject(string table)
        {
            MySqlConnection localConn = OpenConnection();
            var statement = "Select * from " + table + ";";
            var command = new MySqlCommand(statement, localConn);
            using MySqlDataReader rdr = command.ExecuteReader();

            List<GroupProject> list = new List<GroupProject>();
            while (rdr.Read())
            {
                GroupProject gruopProject = new GroupProject();
                gruopProject.ProjectId = rdr.GetInt32(0);
                gruopProject.GroupId = rdr.GetInt32(1);
                list.Add(gruopProject);
            }
            return list;
        }
        public static void CreateRdfFile(List<Person> actors, List<Project> movies,List<Group> categories, List<ProjectActor> moviesActors, List<GroupProject> categoryMovies)
        {
            string path = @"C:\Poli\Master\Anu2\MCSW\proiect\mcsw-project\exports\rdf_generated.rdf";
            File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sw.WriteLine("<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\"");
                    sw.WriteLine("\t \t xmlns:foaf=\"http://xmlns.com/foaf/0.1/\">");
                    sw.WriteLine("\n");
                    foreach(var actor in actors)
                    {
                        sw.WriteLine("  <rdf:Description rdf:about=\"http://ti.etcti.upt.ro/actors/" + actor.FirstName.ToLower() + "-" +actor.LastName.ToLower() + "\">");
                        sw.WriteLine("    <rdf:type rdf:resource=\"http://xmlns.com/foaf/0.1/Person\"/>");
                        sw.WriteLine("    <foaf:givenname>" + actor.FirstName + "</foaf:givenname>");
                        sw.WriteLine("    <foaf:family_name>" + actor.LastName +"</foaf:family_name>");
                        sw.WriteLine("  </rdf:Description>");
                    }

                    sw.WriteLine("\n");
                    foreach (var movie in movies)
                    {
                        sw.WriteLine("  <rdf:Description rdf:about=\"http://ti.etcti.upt.ro/movies/" + movie.ProjectName.Replace(" ", "-").ToLower() + "\">");
                        sw.WriteLine("    <rdf:type rdf:resource=\"http://xmlns.com/foaf/0.1/Project\"/>");
                        sw.WriteLine("    <foaf:title>" + movie.ProjectName + "</foaf:title>");
                        sw.WriteLine("    <foaf:description>" + movie.ProjectDescription + "</foaf:description>");
                        if(!(movie.Actors is null || movie.Actors.Count == 0))
                        {
                            foreach (var actor in movie.Actors)
                            {
                                sw.WriteLine("<foaf:contributors rdf:resource=\"http://ti.etcti.upt.ro/actors/"+ actor.FirstName.ToLower() + "-" + actor.LastName.ToLower() +"\"/>");
                            }
                        }
                        sw.WriteLine("  </rdf:Description>");
                        
                    }
                    sw.WriteLine("\n");
                    foreach (var category in categories)
                    {
                        sw.WriteLine("  <rdf:Description rdf:about=\"http://ti.etcti.upt.ro/categories/" + category.GroupName.Replace(" ", "-").ToLower() + "\">");
                        sw.WriteLine("    <rdf:type rdf:resource=\"http://xmlns.com/foaf/0.1/Project\"/>");
                        sw.WriteLine("    <foaf:name>" + category.GroupName + "</foaf:name>");
                        if (!(category.Movies is null || category.Movies.Count == 0))
                        {
                            foreach (var movie in category.Movies)
                            {
                                sw.WriteLine("<foaf:member rdf:resource=\"http://ti.etcti.upt.ro/movies/" + movie.ProjectName.Replace(" ", "-").ToLower()  + "\"/>");
                            }
                        }
                        sw.WriteLine("  </rdf:Description>");

                    }
                    sw.WriteLine("</rdf:RDF>");

                }
            }
        }

        static void Main()
        {
            var actors = SelectFromTableInModelPerson("actor");
            var movies = SelectFromTableInModelProject("film");
            var categories = SelectFromTableInModelGroup("category");
            var moviesActors = SelectFromTableInModelProjectPerson("film_actor");
            var groupsMovies = SelectFromTableInModelGruopProject("film_category");

             foreach (var actor in actors)
                {
                List<Project> mov = new List<Project>();
                foreach (var movie in movies)
                {
                    foreach(var ma in moviesActors)
                    {
                        if(actor.PersonId == ma.PersonId && movie.ProjectId == ma.ProjectId)
                        {
                            mov.Add(movie);
                        }
                    }
                }
                actor.Movies = mov;
             }

            foreach (var movie in movies)
            {
                List<Person> per = new List<Person>();
                foreach (var actor in actors)
                {
                    foreach (var ma in moviesActors)
                    {
                        if (actor.PersonId == ma.PersonId && movie.ProjectId == ma.ProjectId)
                        {
                            per.Add(actor);
                        }
                    }
                }
                movie.Actors = per;
            }

            foreach (var category in categories)
            {
                List<Project> mov = new List<Project>();
                foreach (var movie in movies)
                {
                    foreach (var gm in groupsMovies)
                    {
                        if (movie.ProjectId == gm.ProjectId && category.GroupId == gm.GroupId)
                        {
                            mov.Add(movie);
                        }
                    }
                }
                category.Movies = mov;
            }

            CreateRdfFile(actors,movies,categories,moviesActors,groupsMovies);
        }
    }
}