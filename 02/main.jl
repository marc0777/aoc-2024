function safe(a)
    if !issorted(a, rev=false) && !issorted(a, rev=true)
       return false
    end

    for i = 2:length(a)
        d = abs(a[i-1] - a[i])
        if d < 1 || d > 3
            return false
        end
    end

    return true
end

open("input.txt") do f
    global c = 0
    global c2 = 0
    while ! eof(f)
        s = readline(f)
        pieces = split(s, ' ', keepempty=false)

        v = Int32[]
        map(pieces) do piece
            v = [v; parse(Int32, piece)]
        end

        if safe(v)
            global c+=1
            global c2+=1
        else
            for i = 1:length(v)
                if safe(deleteat!(copy(v), i))
                    global c2+=1
                    break
                end
            end
        end
    end
    println("$c - $c2")

end