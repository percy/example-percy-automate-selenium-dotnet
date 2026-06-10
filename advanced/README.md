# Advanced Percy on Automate + Selenium-.NET

Exercises the full applicable Percy on Automate feature surface for `PercyIO.Selenium` in Automate mode.

## What this example covers

An NUnit suite (`AdvancedTest.cs`) where each `[Test]` exercises one row of the Percy on Automate matrix using the `Dictionary<string, object>` options overload.

DOM-only options marked `N/A` in `matrix.yml`.

## Run locally

```bash
cd advanced
make install
export BROWSERSTACK_USERNAME="<your username>"
export BROWSERSTACK_ACCESS_KEY="<your access key>"
export PERCY_TOKEN="<your project token>"
make test
```

## CI note

`workflow_dispatch`-only — Percy on Automate CI requires a real BrowserStack Automate session.

## Coverage matrix

Source of truth: [`matrix.yml`](./matrix.yml).
