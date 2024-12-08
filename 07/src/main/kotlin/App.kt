import java.io.File
import java.io.InputStream

fun concatNum(a: Long, b: Long): Long {
    return (a.toString() + b.toString()).toLong()
}

class Expression{
    val elements: MutableList<Long>
    var result: Long

    constructor(s: String) {
        val strs = s.split(": ")
        result = strs[0].toLong()
        elements = mutableListOf<Long>()
        strs[1].split(" ").forEach {
            elements.add(it.toLong())
        }
    }

    fun check(concat: Boolean): Boolean {
        return checkRec(1, elements[0], concat)
    }

    private fun checkRec(pos: Int, cur: Long, concat: Boolean): Boolean {
        if (pos >= elements.size) {
            return cur == result;
        }
        return checkRec(pos+1, cur * elements[pos], concat) || checkRec(pos+1, cur + elements[pos], concat) || (concat && checkRec(pos+1, concatNum(cur, elements[pos]), concat))
    }
}

fun p12(exprList: List<Expression>, concat: Boolean): Long {
    var res: Long = 0
    exprList.forEach {
        if (it.check(concat)) {
            res+=it.result
        }
    }
    return res
}

fun main() {
    val inputStream: InputStream = File("input.txt").inputStream()
    val exprList = mutableListOf<Expression>()
    inputStream.bufferedReader().forEachLine { exprList.add(Expression(it)) }

    println(p12(exprList, false))
    println(p12(exprList, true))
}
