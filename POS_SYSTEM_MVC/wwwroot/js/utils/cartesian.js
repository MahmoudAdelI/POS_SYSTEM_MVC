export default function cartesian(arrays) {
    return arrays.reduce((acc, curr) =>
        acc.flatMap(combo => curr.map(val => [...combo, val]))
        , [[]]);
}