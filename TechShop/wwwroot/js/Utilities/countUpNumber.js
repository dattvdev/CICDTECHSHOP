export default function countUpNumber(targetNumber, counter , duration) {
    let currentNumber = 0;
    const interval = duration / targetNumber;

    const _counter = setInterval(() => {
        if (currentNumber < targetNumber) {
            currentNumber++;
            counter.text(currentNumber);
        } else {
            clearInterval(_counter);
        }
    }, interval);
}