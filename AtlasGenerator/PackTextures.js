var Spritesmith = require('spritesmith');

var fs = require('fs');

var glyphSheetsPath = __dirname + "/../Content/Src/GlyphsSheet/";
var particleSheetsPath = __dirname + "/../Content/Src/ParticlesSheet/";
var targetPath = __dirname + "/../Content/";
fs.readdir(glyphSheetsPath, function(err, items) {
    items = items.map(item => `${glyphSheetsPath}${item}`);
 
    Spritesmith.run({src: items}, function (err, result) {
        fs.writeFileSync(`${targetPath}sheet_glyphs.png`, result.image);
        
        var atlasString = Object.keys(result.coordinates).map(fileName => {
            var data = result.coordinates[fileName];
            fileName = fileName.replace(glyphSheetsPath, '').replace('.png', '');
            
            return `${fileName}|${data.x}|${data.y}|${data.width}|${data.height}`;
        }).join("\n");
        fs.writeFileSync(`${targetPath}sheet_glyphs.txt`, atlasString);
    });
});

fs.readdir(particleSheetsPath, function(err, items) {
    items = items.map(item => `${particleSheetsPath}${item}`);
 
    Spritesmith.run({src: items}, function (err, result) {
        fs.writeFileSync(`${targetPath}sheet_particles.png`, result.image);
        
        var atlasString = Object.keys(result.coordinates).map(fileName => {
            var data = result.coordinates[fileName];
            fileName = fileName.replace(particleSheetsPath, '').replace('.png', '');
            
            return `${fileName}|${data.x}|${data.y}|${data.width}|${data.height}`;
        }).join("\n");
        fs.writeFileSync(`${targetPath}sheet_particles.txt`, atlasString);
    });
});
