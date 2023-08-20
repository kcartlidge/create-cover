# Create Cover

Generate a 900 x 1350 pixel SVG cover.
Supports a small selection of themes using a standard (and auto-arranging) layout.

*(Examples are at the bottom.)*

## To generate a cover

Example command:

```sh
cd <project>
dotnet run -- -titlefontsize=180 -file=../green-example.svg -title="Down\nAmong\nthe\nDead Men" -author="Simon R Green" -series="Forest Kingdom 3" -theme=green -titlefont="Impact" -authorfont="Verdana" -seriesfont="Verdana"
```

## To convert to a PNG

- Open the SVG file in a Chromium browser (eg Brave)
- Right-click on it and choose 'Inspect'
- In the 'Elements' right-click on the SVG node
- Choose 'Capture node screenshot' to save it as a PNG

## Examples

### SVG

![green example](green-example.svg)

### PNG

![green example](green-example.png)
