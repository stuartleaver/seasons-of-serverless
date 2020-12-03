module.exports = {
    chainWebpack: config => {
        config
            .plugin('html')
            .tap(args => {
                args[0].title = "Lovely Ladoo's";
                return args;
            })
    }
}