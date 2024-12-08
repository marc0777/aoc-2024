using System;
using System.IO;
using System.Text;
using System.Linq;

class Map {
    private char [,] matrix;
    private char [,] visits;
    private int curRow;
    private int curCol;
    private char curDir;
    private int initRow;
    private int initCol;
    private char initDir;

    public Map(string path) {
        // Opening the stream and reading it back.
        var lines = File.ReadLines(path);
        // let's assume the map is square...
        var side = lines.Count();

        matrix = new char [side, side];

        var row = 0;
        var col = 0;
        foreach (var line in lines) {
            foreach(char c in line) {
                if (c == 'v' || c == '<' || c == '>' || c == '^') {
                    initCol = col;
                    initRow = row;
                    initDir = c;
                    matrix[row, col] =  '.';
                } else {
                    matrix[row, col] = c;
                }
                col++;
            }
            row++;
            col = 0;
        }
        reset();
    }

    public void reset() {
        (curRow, curCol, curDir) = (initRow, initCol, initDir);
        visits = new char [height(), width()];
        visits[curRow, curCol] = curDir;
    }

    public void where() {
        System.Console.WriteLine("Currently at row " + curRow+ " col "+ curCol + ": " + curDir);
    }

    public (bool, bool, bool, bool) move() {
        var newRow = curRow;
        var newCol = curCol;

        switch(curDir) {
            case 'v':
                newRow++;
                break;
            case '<':
                newCol--;
                break;
            case '>':
                newCol++;
                break;
            case '^':
                newRow--;
                break;
        }
        if (newRow < 0 || newRow >= height() || newCol < 0 || newCol >= width()) {
            return (false, true, false, false);
        }

        if (matrix[newRow, newCol] == '#') {
            return (true, false, false, false);
        }


        (curRow, curCol) = (newRow, newCol);
        bool newVisit = visits[curRow, curCol] == 0;
        bool loop = !newVisit && visits[curRow, curCol] == curDir;

        visits[curRow, curCol] = curDir;
        return (false, false, newVisit, loop);
    }

    public void turn() {
        switch(curDir) {
            case 'v':
                curDir = '<';
                break;
            case '<':
                curDir = '^';
                break;
            case '>':
                curDir = 'v';
                break;
            case '^':
                curDir = '>';
                break;
        }
    }

    public int height() {
        return matrix.GetLength(0);
    }

    public int width() {
        return matrix.GetLength(1);
    }

    public bool checkLoops(int obstRow, int obstCol) {
        char oldVal = matrix[obstRow, obstCol];
        if (oldVal != '.') {
            return false;
        }
        matrix[obstRow, obstCol] = '#';

        bool exit = false;
        bool loop = false;
        while (!exit && !loop) {
            bool obst;
            (obst, exit, _, loop) = move();

            if (obst) {
                turn();
            }

        }
        matrix[obstRow, obstCol] = oldVal;
        return loop;
    }
}

class Program {
    static int P1(Map m) {
        bool exit = false;
        var moves = 1;
        while (!exit) {
            bool obst;
            bool newVisit;

            (obst, exit, newVisit, _) = m.move();

            if (obst) {
                m.turn();
            }

            if (!obst && !exit && newVisit) {
                moves++;
            }
        }
        return moves;
    }

    static int P2(Map m) {
        int res = 0;

        for(int row = 0; row < m.height(); row++) {
            for(int col = 0; col < m.width(); col++) {
                bool loop = m.checkLoops(row, col);
                if (loop) {
                    res++;
                }
                m.reset();
            }
        }

        return res;
    }

    static void Main(string[] args) {
        Map m = new Map("input.txt");
        System.Console.WriteLine("Solution to part 1: "+P1(m));
        m.reset();
        System.Console.WriteLine("Solution to part 2: "+P2(m));
    }
}
