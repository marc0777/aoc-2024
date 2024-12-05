<?php

function load($path) {
    $file = fopen($path,"r");
    $c = 0;
    $r = 0;

    $mat = array (
        array()
    );

    while (! feof($file)) {
        $tc = fgetc($file);
        if ($tc == "\n") {
            $r++;
            $mat[$r] = array();
            $c = 0;
        } else {
            $mat[$r][$c] = $tc;
            $c++;
        }
    }
    fclose($file);
    return $mat;
}


function findXmas($mat, $x, $y, $xDir, $yDir) {
    $xmas = array("X", "M", "A", "S");

    for ($i = 0; $i < count($xmas); $i++) {
        if ($x < 0 || $x >= count($mat) || $y < 0 || $y >= count($mat[$x]) ) {
            return false;
        }

        if ($mat[$x][$y] != $xmas[$i]) {
            return false;
        }

        $x += $xDir;
        $y += $yDir;
    }

    return true;
}

function p1($mat) {
    $count = 0;

    for ($row = 0; $row < count($mat); $row++) {
        for ($col = 0; $col < count($mat[$row]); $col++) {
            for ($i = -1; $i < 2; $i++) {
                for ($j = -1; $j < 2; $j++) {
                    if ($i==0 && $j==0) {
                        continue;
                    }

                    $found = findXmas($mat, $row, $col, $i, $j);
                    if ($found) {
                        $count++;
                    }
                }
            }
        }
    }

    return $count;
}

function findMasMas($mat, $x, $y) {
    if ($mat[$x][$y] != "A") {
        return false;
    }

    if ( $x-1 < 0 || $y-1 < 0 || $x+1 >= count($mat)  || $y+1 >= count($mat[$x]) ) {
        return false;
    }

    for ($i = -1; $i < 2; $i++) {
        for ($j = -1; $j < 2; $j++) {
            if ($i==0 || $j==0) {
                continue;
            }

            if ($mat[$x+$i][$y+$j] == "A" || $mat[$x+$i][$y+$j] == "X" || $mat[$x+$i][$y+$j] == $mat[$x-$i][$y-$j]) {
                return false;
            }
        }
    }

    return true;
}

function p2($mat) {
    $count = 0;

    for ($row = 0; $row < count($mat); $row++) {
        for ($col = 0; $col < count($mat[$row]); $col++) {
            $count += findMasMas($mat, $row, $col);
        }
    }

    return $count;
}

function main() {
    global $mat;
    $mat = load("input.txt");

    echo "Part 1: ".p1($mat)."\n";
    echo "Part 2: ".p2($mat)."\n";
}

main();
?>