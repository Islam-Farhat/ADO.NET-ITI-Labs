using lab2;
using System.Data;

namespace lab2
{
    internal static class CourseBusinessLayer
    {
        public static int Insert(string id, string name, string duration, string topic)
        {
            return DBLayer.DMLCommands($"insert into Course values({id},'{name}',{duration},{topic})");
        }
        public static int update(string id, string name, string duration, string topic)
        {
            return DBLayer.DMLCommands($"update Course set Crs_Name='{name}',Crs_Duration={duration},Top_Id={topic} where Crs_Id={id}");
        }
        public static int delete(string id)
        {
            return DBLayer.DMLCommands($"delete from Course where Crs_Id={id}");
        }
        public static DataTable getAll()
        {
            return DBLayer.select("Course");
        }
    }
}
