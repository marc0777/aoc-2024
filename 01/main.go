package main

import (
	"bufio"
	"fmt"
	"os"
	"slices"
	"strconv"
	"strings"
)

func load(path string) (a, b []int) {
	f, err := os.Open(path)
	if err != nil {
		panic(err)
	}
	defer f.Close()

	s := bufio.NewScanner(f)

	for s.Scan() {
		as, bs, _ := strings.Cut(s.Text(), "   ")
		at, err := strconv.Atoi(as)
		if err != nil {
			panic(err)
		}
		a = append(a, at)
		bt, err := strconv.Atoi(bs)
		if err != nil {
			panic(err)
		}
		b = append(b, bt)
	}
	return a, b
}

func p1(a, b []int) int {
	slices.Sort(a)
	slices.Sort(b)

	var d int
	for i := 0; i < len(a); i++ {
		p := a[i] - b[i]
		if p < 0 {
			p = -p
		}
		d += p
	}
	return d
}

func p2(a, b []int) int {
	slices.Compact(a)
	m := make(map[int]int)

	for i := 0; i < len(a); i++ {
		for j := 0; j < len(b); j++ {
			if b[j] == a[i] {
				p, ok := m[a[i]]
				if ok {
					m[a[i]] = p + 1
				} else {
					m[a[i]] = 1
				}
			}
		}
	}

	var c int
	for k, v := range m {
		c += v * k
	}

	return c
}

func main() {
	fmt.Println("loading data")
	a, b := load("input.txt")

	fmt.Printf("solution to part 1 is: %d\n", p1(a, b))
	fmt.Printf("solution to part 2 is: %d\n", p2(a, b))
}
