
// Change the background according to mood values
function setBackgroundToMood(moodValues) {

    // get string to set css property
    let cssString = createGradientCssString(moodValues);

    // set the body background to the gradient
    body.style.background = cssString;

    console.log(cssString);

}

// Determine the percent for a particular mood
function determineMoodPercent(moodValue, allMoodValues) {

    // calculate total of all moods
    let sumOfMoodValues = 0;
    console.log('Mood values length: ' + allMoodValues.length);
    for (let i = 0; i < allMoodValues.length; i++) {
        console.log('Mood value in values: ' + allMoodValues[i]);
        sumOfMoodValues += allMoodValues[i];
    }

    console.log('Mood value: ' + moodValue);
    console.log('Sum of moods: ' + sumOfMoodValues);

    // determine percentage of chosen mood
    let moodPercent = Math.round((moodValue / sumOfMoodValues) * 100);

    console.log('Mood percent: ' + moodPercent);

    // return the mood percent
    return moodPercent;
}

// Create the string for the gradient
function createGradientCssString(moodValues) { // pass in an object with the mood values

    // get the percent for each mood, store into string partials to concatenate

    // each transition percent should be the previous percent plus the percentage
    // of mood for that color... So the percent of that mood is calculated, then added
    // to the previous transition percent to show the gradient where to change

    // create array with moodValues to pass
    let moodValuesArray = [moodValues.mad, moodValues.confident, moodValues.happy,
        moodValues.hopeful, moodValues.sad, moodValues.scared];

    console.log("Mad: " + moodValues.mad);

    // red: Mad
    let redInitialPercent = 0;
    let redTransitionPercent = determineMoodPercent(moodValues.mad, moodValuesArray);
    let redString = 'rgb(255, 0, 0) ' + redInitialPercent + '%, ' + 'rgb(255, 0, 0) ' + redTransitionPercent + '%';

    // orange: Confident
    let orangeInitialPercent = redTransitionPercent;
    let orangeTransitionPercent = determineMoodPercent(moodValues.confident, moodValuesArray) +
        orangeInitialPercent;
    let orangeString = 'rgb(255, 165, 0) ' + orangeInitialPercent + '%,' + 'rgb(255, 165, 0) ' + orangeTransitionPercent + '%';

    // yellow: Happy
    let yellowInitialPercent = orangeTransitionPercent;
    let yellowTransitionPercent = determineMoodPercent(moodValues.happy, moodValuesArray) +
        yellowInitialPercent;
    let yellowString = 'rgb(255, 255, 0) ' + yellowInitialPercent + '%, ' + 'rgb(255, 255, 0) ' + yellowTransitionPercent + '%';

    // green: Hopeful
    let greenInitialPercent = yellowTransitionPercent;
    let greenTransitionPercent = determineMoodPercent(moodValues.hopeful, moodValuesArray) +
        greenInitialPercent;
    let greenString = 'rgb(0, 128, 0) ' + greenInitialPercent + '%, ' + 'rgb(0, 128, 0) ' + greenTransitionPercent + '%';

    // blue: Sad
    let blueInitialPercent = greenTransitionPercent;
    let blueTransitionPercent = determineMoodPercent(moodValues.sad, moodValuesArray) +
        blueInitialPercent;
    let blueString = 'rgb(0, 0, 255) ' + blueInitialPercent + '%, ' + 'rgb(0, 0, 255) ' + blueTransitionPercent + '%';

    // purple: Scared
    let purpleInitialPercent = blueTransitionPercent;
    //let purpleTransitionPercent = determineMoodPercent(moodValues.scared, moodValuesArray) +
    //    blueTransitionPercent;
    let purpleString = 'rgb(75, 0, 130) ' + purpleInitialPercent + '%, ' + 'rgb(75, 0, 130) 100%';

    // now create the background string
    let cssString = 'linear-gradient(to right, ' + redString + ', ' +
        orangeString + ', ' + yellowString + ', ' + greenString + ', ' +
        blueString + ', ' + purpleString + ')';

    // return the string
    return cssString;

}

// variables
var body = document.body;
var entryArea = document.getElementById('entry-area');

// event handlers
body.onload = setBackgroundToMood(moods);