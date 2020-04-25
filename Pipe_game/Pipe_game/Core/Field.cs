using System;

namespace Pipe_game.Core
{
    public class Field
    {
        private  int rowCount;
    private  int columnCount;

    private  int startRow;
    private  int startColumn;

    private  int endRow;
    private  int endColumn;

    public int score { get; set; }

    private Tile[][] tiles;
    private GameState state;
    
    public int getStartRow() {
             return startRow;
         }
    public int getStartColumn() {
             return startColumn;
         }
    public int getEndRow() {
        return endRow;
    }
    public int getEndColumn() {
        return endColumn;
    }
    public int getRowCount() {
        return rowCount;
    }
    public int getColumnCount() {
        return columnCount;
    }
    public Tile getTile(int row, int column) {
        return tiles[row][column];
    }
    public GameState getState() {
        return state;
    }
    void setState(GameState state) {
        this.state = state;
    }

    public Field(int rowCount, int columnCount)
    {

        if (rowCount < 3 || columnCount < 3)
        {
            Console.WriteLine("Out of bound\n");
        }

        this.startRow = 1;
        this.startColumn = 0;
        this.endRow = 6;
        this.endColumn = 8;
        this.score = 100;

        int[] rows = {1, 0, 0, 0, 0, 1, 2, 3, 3, 4, 5, 5, 5, 5, 4, 3, 3, 4, 5, 6};
        int[] columns = {1, 1, 2, 3, 4, 4, 4, 4, 3, 3, 3, 4, 5, 6, 6, 6, 7, 7, 7, 7};
        PipeFlow[] flow =
        {
            Core.PipeFlow.UpLeft, Core.PipeFlow.UpLeft, Core.PipeFlow.HORIZONTAL,
            Core.PipeFlow.HORIZONTAL, Core.PipeFlow.UpLeft, Core.PipeFlow.VERTICAL,
            Core.PipeFlow.HORIZONTAL, Core.PipeFlow.DownLeft, Core.PipeFlow.DownRight,
            Core.PipeFlow.HORIZONTAL, Core.PipeFlow.UpRight, Core.PipeFlow.VERTICAL,
            Core.PipeFlow.HORIZONTAL, Core.PipeFlow.UpLeft, Core.PipeFlow.VERTICAL,
            Core.PipeFlow.DownLeft, Core.PipeFlow.DownLeft, Core.PipeFlow.VERTICAL,
            Core.PipeFlow.VERTICAL, Core.PipeFlow.DownRight
        };

        this.rowCount = rowCount;
        this.columnCount = columnCount;
        this.tiles = new Tile[rowCount][];
        this.tiles[0] = new Tile[columnCount];
        this.tiles[1] = new Tile[columnCount];
        this.tiles[2] = new Tile[columnCount];
        this.tiles[3] = new Tile[columnCount];
        this.tiles[4] = new Tile[columnCount];
        this.tiles[5] = new Tile[columnCount];
        this.tiles[6] = new Tile[columnCount];

        setLevel(this.startRow,this.startColumn,this.endRow,this.endColumn,rows,columns,flow);
        fillEmptyTiles();

    }
    public void setLevel(int startRow, int startColumn, int endRow, int endColumn, int[] rows,int[] columns, PipeFlow[] flow){
        this.tiles[startRow][startColumn] = new StartTile();
        
        this.tiles[endRow][endColumn] = new EndTile();
        

        for(int i = 0;i<rows.Length;i++){
            this.tiles[rows[i]][columns[i]] = new Pipe();
            this.tiles[rows[i]][columns[i]].setFlow(flow[i]);
        }

    }
    
    static T RandomEnumValue<T> ()
    {
        var v = Enum.GetValues (typeof (T));
        return (T) v.GetValue (new Random ().Next(v.Length));
    }

    public void fillEmptyTiles() {
        for (int row = 0; row < this.getRowCount(); row++) {
            for (int column = 1; column < this.getColumnCount()-1; column++) {
                var value = RandomEnumValue<Core.PipeFlow> ();

                if (this.tiles[row][column] == null) {
                    this.tiles[row][column] = new Pipe();
                    this.tiles[row][column].setFlow(value);
                }
            }
        }
    }

    public void turnTile(int row, int column) {

            if (this.tiles[row][column].getFlow() == PipeFlow.HORIZONTAL)
                this.tiles[row][column].setFlow(PipeFlow.VERTICAL);

            else if (this.tiles[row][column].getFlow() == PipeFlow.VERTICAL)
                this.tiles[row][column].setFlow(PipeFlow.HORIZONTAL);

            else if (this.tiles[row][column].getFlow() == PipeFlow.DownLeft)
                this.tiles[row][column].setFlow(PipeFlow.UpLeft);

            else if (this.tiles[row][column].getFlow() == PipeFlow.UpLeft)
                this.tiles[row][column].setFlow(PipeFlow.UpRight);

            else if (this.tiles[row][column].getFlow() == PipeFlow.UpRight)
                this.tiles[row][column].setFlow(PipeFlow.DownRight);

            else if (this.tiles[row][column].getFlow() == PipeFlow.DownRight)
                this.tiles[row][column].setFlow(PipeFlow.DownLeft);

            --this.score;
            if (this.score == 0)
            {
                this.state = GameState.FAILED;
            }
            
        findConnection(startRow, startColumn, startRow, startColumn+1);
    }

    public void findConnection(int row1, int col1, int row2, int col2) {
        int startRow = row1;
        int startCol = col1;
        int nextRow = row2;
        int nextCol = col2;

        Tile startTile = tiles[startRow][startCol];
        Tile nextTile = tiles[nextRow][nextCol];

        while (nextTile != tiles[endRow][endColumn]) {

            if (nextTile.getFlow() == PipeFlow.HORIZONTAL && nextCol < getColumnCount() - 1 && nextCol > 0) {
                if (tiles[nextRow][nextCol - 1] == startTile) {
                    nextCol++;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else if (tiles[nextRow][nextCol + 1] == startTile) {
                    nextCol--;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else break;

            } else if (nextTile.getFlow() == PipeFlow.VERTICAL && nextRow < getRowCount() - 1 && nextRow > 0) {
                if (tiles[nextRow - 1][nextCol] == startTile) {
                    nextRow++;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else if (tiles[nextRow + 1][nextCol] == startTile) {
                    nextRow--;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else break;

            } else if (nextTile.getFlow() == PipeFlow.UpRight && nextCol < getColumnCount() - 1 && nextRow > 0) {
                if (tiles[nextRow - 1][nextCol] == startTile) {
                    nextCol++;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else if (tiles[nextRow][nextCol + 1] == startTile) {
                    nextRow--;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else break;

            } else if (nextTile.getFlow() == PipeFlow.UpLeft && nextCol > 1 && nextRow > 0) {
                if (tiles[nextRow - 1][nextCol] == startTile) {
                    nextCol--;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else if (tiles[nextRow][nextCol - 1] == startTile) {
                    nextRow--;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else break;

            } else if (nextTile.getFlow() == PipeFlow.DownRight && nextCol < getColumnCount() - 1 && nextRow < getRowCount() - 1) {
                if (tiles[nextRow + 1][nextCol] == startTile) {
                    nextCol++;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else if (tiles[nextRow][nextCol + 1] == startTile) {
                    nextRow++;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else break;

            } else if (nextTile.getFlow() == PipeFlow.DownLeft && nextRow < getRowCount() - 1 && nextCol > 0) {
                if (tiles[nextRow][nextCol - 1] == startTile) {
                    nextRow++;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else if (tiles[nextRow + 1][nextCol] == startTile) {
                    nextCol--;
                    startTile = nextTile;
                    nextTile = tiles[nextRow][nextCol];
                } else break;

            } else break;

            if (nextTile == tiles[endRow][endColumn]) {
                setState(GameState.SOLVED);
            }
        }
    }
    }
}