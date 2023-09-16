using Npgsql;

namespace GreenSale.DataAccess.Repositories;

public class BaseRepository
{
    protected NpgsqlConnection _connection;

    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        //this._connection = new NpgsqlConnection(
        //    "Host=dbaas-db-8432700-do-user-14588616-0.b.db.ondigitalocean.com; Port=25060; Database=greensale-server; " +
        //    " User Id=doadmin; Password=AVNS_7Y14YLpKBS_teIyr_YW;");
        this._connection = new NpgsqlConnection(
            "Host=dbaas-db-8432700-do-user-14588616-0.b.db.ondigitalocean.com; Port=25060; Database=greensale-server; " +
            " User Id=doadmin; Password=AVNS_7Y14YLpKBS_teIyr_YW;");

    }
}