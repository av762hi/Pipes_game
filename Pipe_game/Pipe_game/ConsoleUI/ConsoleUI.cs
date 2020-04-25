using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading;
using Pipe_game.Core;
using Pipe_game.entity;
using Pipe_game.Service;

namespace Pipe_game.ConsoleUI
{
    public class ConsoleUI
    {
         public static void play(Core.Field field){
        
            intro();
       
        do {
            print(field);
            input(field);
        }while(field.getState() == Core.GameState.PLAYING);
        
        print(field);

        if (field.getState() == GameState.FAILED)
        {
            Console.WriteLine("OOPS, YOU LOST THE GAME! YOU ARE OUT OF TURNS!"); 
        }
        else
        {
            Console.WriteLine("YOU WON! YOU ARE THE REAL PLUMBER!");
        }

        Console.WriteLine();
        Service(field);
        
    }


    public static void print(Core.Field field)
    {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("         1      2      3      4      5      6      7");

        char[][] consoleField = new char[field.getRowCount()*3][];
        consoleField[0] = new char[field.getColumnCount()*3];
        consoleField[1] = new char[field.getColumnCount()*3];
        consoleField[2] = new char[field.getColumnCount()*3];
        consoleField[3] = new char[field.getColumnCount()*3];
        consoleField[4] = new char[field.getColumnCount()*3];
        consoleField[5] = new char[field.getColumnCount()*3];
        consoleField[6] = new char[field.getColumnCount()*3];
        consoleField[7] = new char[field.getColumnCount()*3];
        consoleField[8] = new char[field.getColumnCount()*3];
        consoleField[9] = new char[field.getColumnCount()*3];
        consoleField[10] = new char[field.getColumnCount()*3];
        consoleField[11] = new char[field.getColumnCount()*3];
        consoleField[12] = new char[field.getColumnCount()*3];
        consoleField[13] = new char[field.getColumnCount()*3];
        consoleField[14] = new char[field.getColumnCount()*3];
        consoleField[15] = new char[field.getColumnCount()*3];
        consoleField[16] = new char[field.getColumnCount()*3];
        consoleField[17] = new char[field.getColumnCount()*3];
        consoleField[18] = new char[field.getColumnCount()*3];
        consoleField[19] = new char[field.getColumnCount()*3];
        consoleField[20] = new char[field.getColumnCount()*3];

        for (int row = 0; row < field.getRowCount(); row++) {
            for (int column = 0; column < field.getColumnCount(); column++) {
                Core.Tile tile = field.getTile(row, column);

                if(tile == null){}
                else if (tile.getFlow() == PipeFlow.HORIZONTAL){
                    consoleField[row*3+1][column*3+1] = 'o';
                    consoleField[row*3+1][column*3] = 'o';
                    consoleField[row*3+1][column*3+2] = 'o';}
                else if (tile.getFlow() == PipeFlow.VERTICAL){
                    consoleField[row*3][column*3+1] = 'o';
                    consoleField[row*3+2][column*3+1] = 'o';
                    consoleField[row*3+1][column*3+1] = 'o';}
                else if (tile.getFlow() == PipeFlow.UpLeft){
                    consoleField[row*3][column*3+1] = 'o';
                    consoleField[row*3+1][column*3] = 'o';
                    consoleField[row*3+1][column*3+1] = 'o';}
                else if (tile.getFlow() == PipeFlow.DownLeft){
                    consoleField[row*3+1][column*3] = 'o';
                    consoleField[row*3+2][column*3+1] = 'o';
                    consoleField[row*3+1][column*3+1] = 'o';}
                else if (tile.getFlow() == PipeFlow.DownRight){
                    consoleField[row*3+1][column*3+2] = 'o';
                    consoleField[row*3+2][column*3+1] = 'o';
                    consoleField[row*3+1][column*3+1] = 'o';}
                else if (tile.getFlow() == PipeFlow.UpRight){
                    consoleField[row*3][column*3+1] = 'o';
                    consoleField[row*3+1][column*3+2] = 'o';
                    consoleField[row*3+1][column*3+1] = 'o';}
                

            }
        }
        
        consoleField[field.getStartRow()*3+1][field.getStartColumn()*3] = '=';
        consoleField[field.getStartRow()*3+1][field.getStartColumn()*3+1] = '=';
        consoleField[field.getStartRow()*3+1][field.getStartColumn()*3+2] = '>';
        
        consoleField[field.getEndRow()*3+1][field.getEndColumn()*3] = '=';
        consoleField[field.getEndRow()*3+1][field.getEndColumn()*3+1] = '=';
        consoleField[field.getEndRow()*3+1][field.getEndColumn()*3+2] = '>';

        for(int row = 0;row<field.getRowCount()*3;row++){
            for(int column = 0;column<field.getColumnCount()*3;column++){
                char tile = consoleField[row][column];
                
                Console.ForegroundColor = ConsoleColor.Green; 
                Console.Write(tile + " " );
                if (column % 3 == 2 && column != field.getColumnCount() * 3 - 1)
                {
                    Console.ForegroundColor
                        = ConsoleColor.Red;
                    Console.Write('|');
                }
            }
            if(row%3 == 1)
                Console.Write(row/3);
            Console.WriteLine();
            if(row%3 == 2 && row != field.getRowCount()*3){
                Console.Write("     ");
                for(int i = 0;i<field.getColumnCount()*6-1;i++){
                    Console.ForegroundColor 
                        = ConsoleColor.Red; 
                    Console.Write( '_' );}
                Console.WriteLine();
            }
        }
        Console.WriteLine();
            Console.WriteLine("Now you have " + field.score + " free turns.");
        Console.WriteLine();
    }

    public static void input(Field field) {

        Console.ForegroundColor    = ConsoleColor.Blue;
        Console.WriteLine();
        Console.WriteLine("Which tile u wanna turn?");
        Console.WriteLine("Set row:");
        string r = Console.ReadLine();
        int row = Convert.ToInt32(r);
        

        Console.WriteLine("Set column:");
        r = Console.ReadLine();
        int column = Convert.ToInt32(r);

        if((row < 0 || row > field.getRowCount()-1) || (column < 1 || column > field.getColumnCount()-2)){
            Console.WriteLine(ConsoleColor.Red +  "Tile out of field, change your input!" );
             Thread.Sleep(3000);}
        else
            field.turnTile(row,column);

    }

    public static void intro()
    {
        
        Console.WriteLine("\nWELCOME IN GAME PIPES!\n" +
                "Ooops, your water pipes are broken :/ Someone tried to fix the issue,\n" +
                "but he puts pipes in wrong order. You should find the flow from start\n" +
                "to the end by turning pipes.\n" +
                "GOOD LUCK! loading...\n");
    
    Thread.Sleep(10000);

    }

    public static void Service(Field field)
    {
        int rate;
        string name;
        string comm;
        Console.WriteLine("If you enjoyed this game, write down your name, rating and comment ;)");
        Console.WriteLine();
        Console.WriteLine("Your Name:");
        name = Console.ReadLine();
        Console.WriteLine("Your Rating:");
        rate = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Your Comment:");
        comm = Console.ReadLine();
        
        var rating = new Rating {Game = "Pipes", Player = name, Ratings = rate};
        var com = new Comment{Game = "Pipes", Player = name, Text = comm};
        var score = new Score{Game = "Pipes",Player = name,Scores = field.score};
        
        RatingServiceEF ratingService = new RatingServiceEF();
        CommentServiceEF commentService = new CommentServiceEF();
        ScoreServiceEF scoreService = new ScoreServiceEF();
        
        ratingService.AddRating(rating);
        commentService.AddComment(com);
        scoreService.AddScore(score);
        
        Console.WriteLine();
        Console.WriteLine("ALL COMMENTS:");
        Console.WriteLine();

        var comments = commentService.GetComments();
        int comIndex = 1;
        foreach (var comment in comments)
        {
            Console.WriteLine("{0}. {1,-12}    {2,4}", comIndex, comment.Player, comment.Text);
            comIndex++;
        }
        
        Console.WriteLine();
        Console.WriteLine("ALL RATINGS:");
        Console.WriteLine();
        
        var ratings = ratingService.GetRatings();
        int rateIndex = 1;
        foreach (var rat in ratings)
        {
            Console.WriteLine("{0}. {1,-12}    {2,4}", rateIndex, rat.Player, rat.Ratings);
            rateIndex++;
        }
        
        Console.WriteLine();
        Console.WriteLine("ALL SCORES:");
        Console.WriteLine();
        
        var scores = scoreService.GetScores();
        int scrIndex = 1;
        foreach (var scor in scores)
        {
            Console.WriteLine("{0}. {1,-12}    {2,4}", scrIndex, scor.Player, scor.Scores);
            scrIndex++;
        }

    }
    
    }
}