using BlazorSongList.Models;
using BlazorSongList.Utility;
using Dapper;
using MySql.Data.MySqlClient; // Keep this for MySqlConnection
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SongRepository
{
    public async Task<List<Song>> GetAllSongsAsync()
    {
        var result = new List<Song>();
        try
        {
            var query = "SELECT * FROM songs";
            using (var dbConnection = new MySqlConnection(ConfigHelper.Db))
            {
                var queryResult = await dbConnection.QueryAsync<dynamic>(query);
                foreach (var item in queryResult)
                {
                    var data = new Song
                    {
                        SongId = item.SongId,
                        Title = item.Title,
                        Artist = item.Artist,
                        Year = item.Year
                    };
                    result.Add(data);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
            throw;
        }
        return result;
    }

    public async Task InsertSongAsync(Song song)
    {
        try
        {
            var query = "INSERT INTO songs (SongId, Title, Artist, Year) VALUES (@SongId, @Title, @Artist, @Year)";
            using (var dbConnection = new MySqlConnection(ConfigHelper.Db))
            {
                await dbConnection.ExecuteAsync(query, song);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
            throw;
        }
    }

    public async Task UpdateSongAsync(Song song)
    {
        try
        {
            var query = "UPDATE songs SET Title = @Title, Artist = @Artist, Year = @Year WHERE SongId = @SongId";
            using (var dbConnection = new MySqlConnection(ConfigHelper.Db))
            {
                await dbConnection.ExecuteAsync(query, song);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
            throw;
        }
    }

    public async Task DeleteSongAsync(string songId)
    {
        try
        {
            var query = "DELETE FROM songs WHERE SongId = @SongId";
            using (var dbConnection = new MySqlConnection(ConfigHelper.Db))
            {
                await dbConnection.ExecuteAsync(query, new { SongId = songId });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
            throw;
        }
    }
}
