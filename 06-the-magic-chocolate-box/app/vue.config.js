module.exports = {
    chainWebpack: config => {
        config
            .plugin('html')
            .tap(args => {
                args[0].title = "The Magic Chocolate Box";
                return args;
            })
    }
}