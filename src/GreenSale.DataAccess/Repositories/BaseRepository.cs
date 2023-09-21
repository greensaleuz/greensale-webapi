using Npgsql;

namespace GreenSale.DataAccess.Repositories;

public class BaseRepository
{
    protected NpgsqlConnection _connection;

    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        // string connection = "Host=dbaas-db-8432700-do-user-14588616-0.b.db.ondigitalocean.com; Port=25060; Database=greensale-server; User Id=doadmin; Password=AVNS_7Y14YLpKBS_teIyr_YW;";
        string connection = "Host = localhost; Database = greensale-db; Port = 5432; User Id = postgres; Password = 1234";
        this._connection = new NpgsqlConnection(connection);
    }
}